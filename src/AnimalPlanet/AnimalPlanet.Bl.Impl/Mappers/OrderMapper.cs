using AnimalPlanet.Bl.Abstract.Mappers;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Impl.Mappers
{
    public class OrderMapper : IMapper<Order, OrderModel>
    {
        public OrderModel Map(Order entity)
        {
            return new OrderModel
            {
                Id = entity.Id,
                Denomination = entity.Denomination,
                ClassId = entity.ClassId,
                ClassDenomination = entity.Class?.Denomination
            };
        }

        public Order MapBack(OrderModel model)
        {
            return new Order
            {
                Denomination = model.Denomination,
                ClassId = model.ClassId,
            };
        }

        public Order MapUpdate(Order entity, OrderModel model)
        {
            entity.Denomination = model.Denomination;
            entity.ClassId = model.ClassId;

            return entity;
        }
    }
}
