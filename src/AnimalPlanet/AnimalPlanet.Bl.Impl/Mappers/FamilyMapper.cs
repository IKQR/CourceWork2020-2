using AnimalPlanet.Bl.Abstract.Mappers;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Impl.Mappers
{
    public class FamilyMapper : IMapper<Family, FamilyModel>
    {
        public FamilyModel Map(Family entity)
        {
            return new FamilyModel
            {
                Id = entity.Id,
                Denomination = entity.Denomination,
                OrderId = entity.OrderId,
                OrderDenomination = entity.Order?.Denomination
            };
        }

        public Family MapBack(FamilyModel model)
        {
            return new Family
            {
                Denomination = model.Denomination,
                OrderId = model.OrderId,
            };
        }

        public Family MapUpdate(Family entity, FamilyModel model)
        {
            entity.Denomination = model.Denomination;
            entity.OrderId = model.OrderId;

            return entity;
        }
    }
}
