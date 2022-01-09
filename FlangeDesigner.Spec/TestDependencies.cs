using System.IO;
using FlangeDesigner.Main.Application;
using FlangeDesigner.Main.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;

namespace FlangeDesigner.Spec
{
    public class TestDependencies
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Spec.json", false, true);
            var configuration = builder.Build();
            services.AddInfrastructure(configuration);
            services.AddApplication(configuration);

            return services;
        }
    }
}