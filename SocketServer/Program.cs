using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SockerServer.Data.Context;
using SockerServer.Data.Extentions;
using SocketServer.Extentions;
using SocketServer.Services;
using SocketServer.Singletons;
using System.Reflection;

class Program
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        builder.Services.AddSocketServerRepositories();

        builder.Services.AddSocketServerDbContext();
        builder.Services.AddServices();
        builder.Services.AddControllers();
        builder.Services.AddOptions(config);

        builder.Build();
        
        SocketServer.Extentions.ServiceCollection.Services = builder.Services.BuildServiceProvider();

        var rs = SocketServer.Extentions.ServiceCollection.Services.GetService<IRoutingRegistry>();
        rs!.RegisterRoutesFromAssembly(Assembly.GetExecutingAssembly());

        var context = SocketServer.Extentions.ServiceCollection.Services.GetService<SocketServerDbContext>();
        
        context!.Database.Migrate();

        AsyncSocketListener.Instance.StartListening();
    }
}
