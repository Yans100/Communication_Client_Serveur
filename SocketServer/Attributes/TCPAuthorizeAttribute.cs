using SocketServer.Enums;

namespace SocketServer.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TCPAuthorizeAttribute : Attribute
    {
        public List<Role> Roles { get; } = [];

        public TCPAuthorizeAttribute(params Role[] roles)
        {
            Roles = [.. roles];
        }
    }
}
