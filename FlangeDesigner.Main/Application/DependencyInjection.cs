using FlangeDesigner.AbstractEngine;
using FlangeDesigner.Main.Application.Project;
using FlangeDesigner.SolidWorksEngine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlangeDesigner.Main.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEngine, Engine>();
            services.AddTransient<IProjectFacade, ProjectFacade>();
            services.AddTransient<ProjectLoader, ProjectLoader>();

            return services;
        }
    }
}