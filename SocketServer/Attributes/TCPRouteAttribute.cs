namespace SocketServer.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class TCPRouteAttribute : Attribute
    {
        public string Route { get; }

        public TCPRouteAttribute(string route)
        {
            Route = route;
        }
    }
}
