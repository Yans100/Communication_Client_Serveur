namespace SocketServer.Singletons
{
    public interface IAsyncSocketListener : IDisposable
    {
        void StartListening();
        void Send(int id, string msg, bool close);
        void CloseClient(int id);
    }
}
