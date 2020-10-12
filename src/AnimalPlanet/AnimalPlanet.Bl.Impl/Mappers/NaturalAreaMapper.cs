using AnimalPlanet.Bl.Abstract.Mappers;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Impl.Mappers
{
    public class NaturalAreaMapper : IMapper<NaturalArea, NaturalAreaModel>
    {
        public NaturalAreaModel Map(NaturalArea entity)
        {
            return new NaturalAreaModel
            {
                Id = entity.Id,
                Denomination = entity.Denomination,
            };
        }

        public NaturalArea MapBack(NaturalAreaModel model)
        {
            return new NaturalArea
            {
                Denomination = model.Denomination,
            };
        }

        public NaturalArea MapUpdate(NaturalArea entity, NaturalAreaModel model)
        {
            entity.Denomination = model.Denomination;

            return entity;
        }
    }
}
