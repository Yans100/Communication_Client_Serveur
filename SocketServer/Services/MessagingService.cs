using Newtonsoft.Json;
using SocketServer.Attributes;
using SocketServer.Enums;
using SocketServer.Models;
using SocketServer.Singletons;
using System.Net.Http.Headers;

namespace SocketServer.Services
{
    internal class MessagingService : IMessagingService
    {
        private readonly IRoutingRegistry routingService;
        private readonly IAuthorizationService authorizationService;

        public MessagingService(IRoutingRegistry routingService, IAuthorizationService authorizationService)
        {
            this.routingService = routingService ?? throw new ArgumentNullException(nameof(routingService));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task HandleMessageAsync(string msg, int clientId)
        {
            var elements = msg.Split('|').ToList();
            elements = [.. elements.Select(e => e.Trim())];

            var args = elements.Skip(1).ToArray();

            var authRules = routingService.GetAuthRules(elements[0]);
            IResponseObject res = HandleAuth(authRules, args);

            if (res.ResponseState == ResponseState.Success)
            {
                if (authRules is not null)
                    args = [.. args.Skip(1)];
                res = await InvokeRouteAsync(elements[0], args);
            }

            SendJsonResponse(res, clientId);
        }

        private IResponseObject HandleAuth(TCPAuthorizeAttribute? rules, string[] args)
        {
            if (rules == null)
                return new ResponseObject(ResponseState.Success, string.Empty);
            if (args.Length == 0)
                return new ResponseObject(ResponseState.Unauthorized, "Unauthorized");

            var authorized = authorizationService.IsAuthorized(rules.Roles, args[0]);

            return authorized ? new ResponseObject(ResponseState.Success, string.Empty) : new ResponseObject(ResponseState.Unauthorized, "Unauthorized");
        }

        private async Task<IResponseObject> InvokeRouteAsync(string route, string[] args)
        {
            var method = routingService.GetRoute(route);
            if (method == null)
                return new ResponseObject(ResponseState.InvalidRequest, "Invalid Route");

            var instance = Extentions.ServiceCollection.GetService(method.DeclaringType!) ?? throw new Exception("Service of type " + method.DeclaringType!.FullName + " is not registered");

            object?[] parameters = [.. method.GetParameters().Select((p, i) => System.Text.Json.JsonSerializer.Deserialize(args[i], p.ParameterType))];

            var result = method.Invoke(instance, parameters);

            if(result is Task taskRes)
            {
                await taskRes.ConfigureAwait(false);

                var resProp = taskRes.GetType().GetProperty("Result");
                var res = resProp?.GetValue(taskRes);

                result = resProp?.GetValue(taskRes);
            }

            if (result is IResponseObject response)
                return response;

            return new ResponseObject(ResponseState.Success, result);
        }

        public void SendJsonResponse(IResponseObject element, int clientId)
        {
            string json = JsonConvert.SerializeObject(element);
            AsyncSocketListener.Instance.Send(clientId, json, true);
        }
    }
}
