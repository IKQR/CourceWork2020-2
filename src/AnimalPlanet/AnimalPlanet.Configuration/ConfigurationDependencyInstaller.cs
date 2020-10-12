using Microsoft.Extensions.DependencyInjection;

namespace AnimalPlanet.Configuration
{
    public static class ConfigurationDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddTransient<PaginationConfiguration>();
        }
    }
}
