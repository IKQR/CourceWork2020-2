using AnimalPlanet.Bl.Abstract.Mappers;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Impl.Mappers
{
    public class ClassMapper : IMapper<Class, ClassModel>
    {
        public ClassModel Map(Class entity)
        {
            return new ClassModel
            {
                Id = entity.Id,
                Denomination = entity.Denomination,
                PhylumId = entity.PhylumId,
                PhylumDenomination = entity.Phylum?.Denomination
            };
        }

        public Class MapBack(ClassModel model)
        {
            return new Class
            {
                Denomination = model.Denomination,
                PhylumId = model.PhylumId,
            };
        }

        public Class MapUpdate(Class entity, ClassModel model)
        {
            entity.Denomination = model.Denomination;
            entity.PhylumId = model.PhylumId;

            return entity;
        }
    }
}
