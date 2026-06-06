using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SockerServer.Data.Repositories;
using SocketServer.Enums;
using SocketServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocketServer.Services
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly IPersonRepository personRepository;
        private readonly IConfiguration configuration;

        public AuthorizationService(IPersonRepository personRepository, IConfiguration configuration)
        {
            this.personRepository = personRepository;
            this.configuration = configuration;
        }

        public AuthToken GetAuthorizationToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, null, DateTime.Now.AddHours(1), credentials);

            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<IEnumerable<Claim>?> GetClaimsAsync(AuthRequest request)
        {
            Role role;
            if (!IsAdmin(request))
            {
                role = Role.User;

                var people = await personRepository.GetPeopleByFirstNameAsync(request.Username);
                var person = people.FirstOrDefault(p => p.Password == request.Password);
                if (person is null || person.IsOnRedList)
                    return null;

            }
            else
                role = Role.Admin;

            List<Claim> claims =
                [
                    new Claim(ClaimTypes.Role, role.ToString()),
                    new Claim(ClaimTypes.Name, request.Username),
                ];

            return claims;
        }

        private bool IsAdmin(AuthRequest request) => request.Username == "admin" && request.Password == "admin";

        public bool IsAuthorized(List<Role> authorizedRoles, string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                if (handler.ReadToken(token) is not JwtSecurityToken jsonToken)
                    return false;

                var claims = jsonToken.Claims.Select(claim => new { claim.Type, claim.Value });
                var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (role is null)
                    return false;

                var valid = Enum.TryParse(role, out Role roleValue);
            
                if (!valid || !authorizedRoles.Contains(roleValue))
                    return false;

                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}
