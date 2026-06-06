using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using SockerServer.Data.Context;
using SockerServer.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SockerServer.Data.Extentions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddSocketServerRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }

        public static IServiceCollection AddSocketServerDbContext(this IServiceCollection services)
        {
            var conn = new SqliteConnection("Filename=:memory:");
            conn.Open();

            services.AddDbContext<SocketServerDbContext>(options =>
            {
                options.UseSqlite(conn);
            });

            return services;
        }
    }
}
