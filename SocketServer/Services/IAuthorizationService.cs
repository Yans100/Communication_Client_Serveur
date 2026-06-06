using SocketServer.Enums;
using SocketServer.Models;
using System.Security.Claims;

namespace SocketServer.Services
{
    internal interface IAuthorizationService
    {
        bool IsAuthorized(List<Role> authorizedRoles, string token);
        AuthToken GetAuthorizationToken(IEnumerable<Claim> claims);
        Task<IEnumerable<Claim>?> GetClaimsAsync(AuthRequest request);
    }
}
