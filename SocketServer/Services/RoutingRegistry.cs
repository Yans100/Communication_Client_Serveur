using SocketServer.Attributes;
using System.Reflection;

namespace SocketServer.Services
{
    internal class RoutingRegistry : IRoutingRegistry
    {
        private Dictionary<string, MethodInfo> routes = [];
        private Dictionary<string, TCPAuthorizeAttribute?> authRules = [];

        public void RegisterRoutesFromAssembly(Assembly assembly)
        {
            var methods = assembly.GetTypes()
                .Where(t => t.GetCustomAttributes<TCPControllerAttribute>().Any())
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                .Where(m => m.GetCustomAttribute<TCPRouteAttribute>() is not null);

            foreach (var method in methods)
            {
                var routeAttr = method.GetCustomAttribute<TCPRouteAttribute>();

                routes[routeAttr!.Route] = method;
                authRules[routeAttr!.Route] = method.GetCustomAttribute<TCPAuthorizeAttribute>();
            }
        }

        public TCPAuthorizeAttribute? GetAuthRules(string route)
        {
            var found = authRules.TryGetValue(route, out var authRule);
            return found ? authRule : null;
        }

        public MethodInfo? GetRoute(string route)
        {
            var found = routes.TryGetValue(route, out var routeMethod);
            return found ? routeMethod : null;
        }
    }
}
