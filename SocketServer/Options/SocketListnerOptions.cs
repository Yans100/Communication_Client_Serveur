namespace SocketServer.Options
{
    internal sealed class SocketListnerOptions
    {
        public int ClientCountLimit { get; set; }
        public int Port { get; set; }
        public string HostName { get; set; } = "localhost";
    }
}
