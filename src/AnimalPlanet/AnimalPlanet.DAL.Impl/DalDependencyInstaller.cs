using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Impl.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalPlanet.DAL.Impl
{
    public static class DalDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddTransient<IClassRepository, ClassRepository>();
            services.AddTransient<IFamilyRepository, FamilyRepository>();
            services.AddTransient<IGenusRepository, GenusRepository>();
            services.AddTransient<INaturalAreaRepository, NaturalAreaRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPhylumRepository, PhylumRepository>();
            services.AddTransient<IReserveRepository, ReserveRepository>();
            services.AddTransient<ISpecieRepository, SpecieRepository>();
        }
    }
}
