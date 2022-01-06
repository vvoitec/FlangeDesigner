using FlangeDesigner.Main.Domain.Repositories;
using FlangeDesigner.Main.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlangeDesigner.Main.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DapperRepositoryOptions>(configuration.GetSection(DapperRepositoryOptions.Position));
            services.AddTransient<IProjectRepository, DapperProjectRepository>();

            return services;
        }
    }
}