using AnimalPlanet.Bl.Abstract.Mappers;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Impl.Mappers
{
    public class PhylumMapper : IMapper<Phylum, PhylumModel>
    {
        public PhylumModel Map(Phylum entity)
        {
            return new PhylumModel
            {
                Id = entity.Id,
                Denomination = entity.Denomination,
            };
        }

        public Phylum MapBack(PhylumModel model)
        {
            return new Phylum
            {
                Denomination = model.Denomination,
            };
        }

        public Phylum MapUpdate(Phylum entity, PhylumModel model)
        {
            entity.Denomination = model.Denomination;

            return entity;
        }
    }
}
