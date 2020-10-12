using AnimalPlanet.Bl.Abstract.Mappers;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Impl.Mappers
{
    public class GenusMapper : IMapper<Genus, GenusModel>
    {
        public GenusModel Map(Genus entity)
        {
            return new GenusModel
            {
                Id = entity.Id,
                Denomination = entity.Denomination,
                FamilyId = entity.FamilyId,
                FamilyDenomination = entity.Family?.Denomination
            };
        }

        public Genus MapBack(GenusModel model)
        {
            return new Genus
            {
                Denomination = model.Denomination,
                FamilyId = model.FamilyId,
            };
        }

        public Genus MapUpdate(Genus entity, GenusModel model)
        {
            entity.Denomination = model.Denomination;
            entity.FamilyId = model.FamilyId;

            return entity;
        }
    }
}
