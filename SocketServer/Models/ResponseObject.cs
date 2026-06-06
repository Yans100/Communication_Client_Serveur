using SocketServer.Enums;

namespace SocketServer.Models
{
    public class ResponseObject<T> : IResponseObject where T : class
    {
        public ResponseState ResponseState { get; set; }
        public T? Content { get; set; }
        public string? ErrorMessage { get; set; }

        public ResponseObject(ResponseState state, string? errorMessage)
        {
            ResponseState = state;
            ErrorMessage = errorMessage;
        }

        public ResponseObject(ResponseState state, T content)
        {
            ResponseState = state;
            Content = content;
        }
    }

    public class ResponseObject : IResponseObject
    {
        public ResponseState ResponseState { get; set; }
        public object? Content { get; set; }
        public string? ErrorMessage { get; set; }

        public ResponseObject(ResponseState state, string? errorMessage)
        {
            ResponseState = state;
            ErrorMessage = errorMessage;
        }

        public ResponseObject(ResponseState state, object? content)
        {
            ResponseState = state;
            Content = content;
        }
    }

}
