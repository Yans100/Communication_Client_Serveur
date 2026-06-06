using SocketServer.Attributes;
using SocketServer.Models;
using SocketServer.Services;

namespace SocketServer.Controllers
{
    [TCPController]
    internal class AuthController
    {
        private readonly IAuthorizationService authorizationService;

        public AuthController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [TCPRoute("Login")]
        public async Task<ResponseObject<AuthToken>> Login(AuthRequest request)
        {
            if (request == null)
                return new ResponseObject<AuthToken>(Enums.ResponseState.InvalidRequest, "Invalid Request");

            var claims = await authorizationService.GetClaimsAsync(request);
            
            if (claims is null || !claims.Any())
                return new ResponseObject<AuthToken>(Enums.ResponseState.NotFound, "User not found or on Red List");

            var token = authorizationService.GetAuthorizationToken(claims);

            return new ResponseObject<AuthToken>(Enums.ResponseState.Success, token);
        }
    }
}
