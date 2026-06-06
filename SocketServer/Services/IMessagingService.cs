namespace SocketServer.Services
{
    public interface IMessagingService
    {
        Task HandleMessageAsync(string msg, int clientId);
    }
}
