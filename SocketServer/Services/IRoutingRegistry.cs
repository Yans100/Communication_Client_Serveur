using SocketServer.Attributes;
using SocketServer.Models;
using System.Reflection;

namespace SocketServer.Services
{
    public interface IRoutingRegistry
    {
        void RegisterRoutesFromAssembly(Assembly assembly);
        MethodInfo? GetRoute(string route);
        TCPAuthorizeAttribute? GetAuthRules(string route);
    }
}
