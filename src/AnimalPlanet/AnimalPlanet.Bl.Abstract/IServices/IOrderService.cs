using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Abstract.IServices
{
    public interface IOrderService
    {
        public Task<DataResult<List<OrderModel>>> GetPartOfOrders(int skip, int take);
        public Task<DataResult<OrderModel>> GetOrderById(int id);
        public Task<Result> UpdateOrder(int id, OrderModel model);
        public Task<DataResult<OrderModel>> CreateOrder(OrderModel model);
        public Task<Result> DeleteOrder(int id);
    }
}
