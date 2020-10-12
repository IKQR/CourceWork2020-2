using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AnimalPlanet.Bl.Abstract.IServices;
using AnimalPlanet.Bl.Abstract.Mappers;
using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;

using Microsoft.Extensions.Logging;

namespace AnimalPlanet.Bl.Impl.Service
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper<Order, OrderModel> _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderService(
            ILogger<OrderService> logger,
            IMapper<Order, OrderModel> mapper,
            IOrderRepository orderRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<DataResult<List<OrderModel>>> GetPartOfOrders(int skip, int take)
        {
            try
            {
                List<Order> entities = await _orderRepository.GetPart(skip, take);

                List<OrderModel> models = entities.Select(_mapper.Map).ToList();

                return new DataResult<List<OrderModel>>
                {
                    Success = true,
                    Data = models,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problems with getting all orderes");
                return new DataResult<List<OrderModel>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<OrderModel>> GetOrderById(int id)
        {
            try
            {

                Order entity = await _orderRepository.GetById(id);

                if (entity == null)
                {
                    return new DataResult<OrderModel>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                OrderModel model = _mapper.Map(entity);

                return new DataResult<OrderModel>
                {
                    Success = true,
                    Data = model,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with getting Order by id : {id}");
                return new DataResult<OrderModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> UpdateOrder(int id, OrderModel model)
        {
            try
            {
                Order entity = await _orderRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _orderRepository.Update(_mapper.MapUpdate(entity, model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with updating Order by id : {id}");
                return new DataResult<OrderModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<OrderModel>> CreateOrder(OrderModel model)
        {
            try
            {
                Order entity = _mapper.MapBack(model);

                DataResult<Order> result = await _orderRepository.Add(entity);

                return new DataResult<OrderModel>
                {
                    Success = result.Success,
                    ErrorCode = result.ErrorCode,
                    Data = _mapper.Map(result.Data),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with creating Order");
                return new DataResult<OrderModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> DeleteOrder(int id)
        {
            try
            {
                Order entity = await _orderRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _orderRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with deleting Order by id : {id}");
                return new Result
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }
    }
}
