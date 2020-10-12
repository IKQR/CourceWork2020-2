using AnimalPlanet.Bl.Abstract.Mappers;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Impl.Mappers
{
    public class ReserveMapper : IMapper<Reserve, ReserveModel>
    {
        public ReserveModel Map(Reserve entity)
        {
            return new ReserveModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Longitude = entity.Longitude,
                Latitude = entity.Latitude,
            };
        }

        public Reserve MapBack(ReserveModel model)
        {
            return new Reserve
            {
                Name = model.Name,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
            };
        }

        public Reserve MapUpdate(Reserve entity, ReserveModel model)
        {
            entity.Name = model.Name;
            entity.Longitude = model.Longitude;
            entity.Latitude = model.Latitude;

            return entity;
        }
    }
}
