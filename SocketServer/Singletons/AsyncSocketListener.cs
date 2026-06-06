using Microsoft.Extensions.Options;
using SocketServer.Models;
using SocketServer.Options;
using SocketServer.Services;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer.Singletons
{
    internal class AsyncSocketListener : IAsyncSocketListener
    {
        private static readonly ManualResetEvent mre = new(false);
        private static readonly IAsyncSocketListener instance = new AsyncSocketListener();

        private readonly SocketListnerOptions socketListnerOptions;
        private readonly IMessagingService messagingService;

        private readonly IDictionary<int, IStateObject> clients = new Dictionary<int, IStateObject>();

        private AsyncSocketListener()
        {
            messagingService = Extentions.ServiceCollection.GetService<IMessagingService>();
            socketListnerOptions = Extentions.ServiceCollection.GetService<SocketListnerOptions>();
        }

        public static IAsyncSocketListener Instance => instance;

        public void StartListening()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(socketListnerOptions.HostName);
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            IPEndPoint iPEndPoint = new(ipAddress, socketListnerOptions.Port);

            using Socket listener = new(
                iPEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            listener.Bind(iPEndPoint);
            listener.Listen(socketListnerOptions.ClientCountLimit);

            while (true) 
            {
                mre.Reset();
                listener.BeginAccept(OnClientConnected, listener);
                mre.WaitOne();
            }
        }

        private void OnClientConnected(IAsyncResult result)
        {
            mre.Set();

            if (result.AsyncState is not Socket server)
                return;

            StateObject so;

            lock (clients) 
            {
                var id = clients.Any() ? clients.Keys.Max() + 1 : 0;
                Socket client = server.EndAccept(result);
                
                so = new StateObject() { Socket = client, Id = id };
                clients.Add(id, so);
            }

            so.Socket.BeginReceive(so.Buffer, 0, so.BufferSize, SocketFlags.None, OnReadData, so);
        }

        private async void OnReadData(IAsyncResult result)
        {
            if (result.AsyncState is not IStateObject so)
                return;

            var receive = so.Socket.EndReceive(result);
            if(receive > 0)
                so.StringBuider.Append(Encoding.UTF8.GetString(so.Buffer, 0, receive));
            if (receive == so.BufferSize)
                so.Socket.BeginReceive(so.Buffer, 0, so.BufferSize, SocketFlags.None, OnReadData, so);
            else
                await messagingService.HandleMessageAsync(so.StringBuider.ToString(), so.Id);
        }

        public void Send(int id, string msg, bool close)
        {
            clients.TryGetValue(id, out IStateObject? so);
            if (so is null)
                throw new Exception("Invalid Client");

            var msgBytes = Encoding.UTF8.GetBytes(msg);

            so.Close = close;
            so.Socket.BeginSend(msgBytes, 0, msgBytes.Length, SocketFlags.None, SendCallback, so);
        }

        private void SendCallback(IAsyncResult result)
        {
            if (result.AsyncState is not IStateObject so)
                return;

            if (so.Close)
                CloseClient(so.Id);
        }

        public void CloseClient(int id)
        {
            clients.TryGetValue(id, out IStateObject? so);
            if (so is null)
                throw new Exception("Invalid Client");

            so.Socket.Shutdown(SocketShutdown.Both);
            so.Socket.Close();
            so.Socket.Dispose();

            lock (clients) 
            {
                clients.Remove(so.Id);
            }
        }
        public void Dispose()
        {
            foreach (var id in clients.Keys)
                CloseClient(id);

            mre.Dispose();
        }
    }
}
