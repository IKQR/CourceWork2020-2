using AnimalPlanet.Bl.Abstract.IServices;
using AnimalPlanet.Bl.Abstract.Mappers;
using AnimalPlanet.Bl.Impl.Mappers;
using AnimalPlanet.Bl.Impl.Service;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalPlanet.Bl.Impl
{
    public static class BlDependencyInstaller
    {
        public static void Install(IServiceCollection services)
        {
            #region services

            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<IFamilyService, FamilyService>();
            services.AddTransient<IGenusService, GenusService>();
            services.AddTransient<INaturalAreaService, NaturalAreaService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPhylumService, PhylumService>();
            services.AddTransient<IReserveService, ReserveService>();
            services.AddTransient<ISpecieService, SpecieService>();

            #endregion

            #region mappers

            services.AddTransient<IMapper<Phylum, PhylumModel>, PhylumMapper>();
            services.AddTransient<IMapper<Class, ClassModel>, ClassMapper>();
            services.AddTransient<IMapper<Order, OrderModel>, OrderMapper>();
            services.AddTransient<IMapper<Family, FamilyModel>, FamilyMapper>();
            services.AddTransient<IMapper<Genus, GenusModel>, GenusMapper>();
            services.AddTransient<IMapper<Specie, SpecieCreateModel>, SpecieCreateMapper>();

            services.AddTransient<IMapper<NaturalArea, NaturalAreaModel>, NaturalAreaMapper>();
            services.AddTransient<IMapper<Reserve, ReserveModel>, ReserveMapper>();

            #endregion
        }
    }
}
