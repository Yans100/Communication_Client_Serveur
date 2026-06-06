using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocketServer.Controllers;
using SocketServer.Factory;
using SocketServer.Mappers;
using SocketServer.Options;
using SocketServer.Services;

namespace SocketServer.Extentions
{
    internal static class ServiceCollection
    {
        public static IServiceProvider? Services { get; set; }

        public static T GetService<T>() where T : class
        {
            return Services?.GetRequiredService<T>()!;
        }

        public static object GetService(Type type)
        {
            return Services?.GetRequiredService(type)!;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IMessagingService, MessagingService>();
            services.AddSingleton<IRoutingRegistry, RoutingRegistry>();
            services.AddSingleton<IBuisnessFieldMapper, BuisnessFieldMapper>();
            services.AddSingleton<ICategoryMapper, CategoryMapper>();
            services.AddSingleton<IStudentMapper, StudentMapper>();
            services.AddSingleton<ITeacherMapper, TeacherMapper>();
            services.AddSingleton<ITeachingAssistantMapper, TeachingAssistantMapper>();
            services.AddSingleton<IPersonMapperFactory, PersonMapperFactory>();

            return services;
        }

        public static IServiceCollection AddControllers(this IServiceCollection services)
        {
            services.AddScoped<PersonController>();
            services.AddScoped<StudentController>();
            services.AddScoped<TeacherController>();
            services.AddScoped<TeachingAssistantController>();
            services.AddScoped<AuthController>();

            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            SocketListnerOptions options = new();

            configuration.GetSection(nameof(SocketListnerOptions))
                .Bind(options);

            services.AddSingleton(options);

            return services;
        }
    }
}
