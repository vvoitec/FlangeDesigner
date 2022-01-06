using System.IO;
using FlangeDesigner.Main.Application;
using FlangeDesigner.Main.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace FlangeDesigner.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json", false, true);
            var configuration = builder.Build();
            services.AddInfrastructure(configuration);
            services.AddApplication(configuration);
        }
    }
}