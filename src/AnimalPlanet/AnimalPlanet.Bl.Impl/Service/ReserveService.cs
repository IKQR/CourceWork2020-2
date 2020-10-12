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
    public class ReserveService : IReserveService
    {
        private readonly ILogger<ReserveService> _logger;
        private readonly IMapper<Reserve, ReserveModel> _mapper;
        private readonly IReserveRepository _reserveRepository;

        public ReserveService(
            ILogger<ReserveService> logger,
            IMapper<Reserve, ReserveModel> mapper,
            IReserveRepository reserveRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _reserveRepository = reserveRepository;
        }

        public async Task<DataResult<List<ReserveModel>>> GetPartOfReserves(int skip, int take)
        {
            try
            {
                List<Reserve> entities = await _reserveRepository.GetPart(skip, take);

                List<ReserveModel> models = entities.Select(_mapper.Map).ToList();

                return new DataResult<List<ReserveModel>> { Success = true, Data = models, };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problems with getting all Reserves");
                return new DataResult<List<ReserveModel>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<ReserveModel>> GetReserveById(int id)
        {
            try
            {

                Reserve entity = await _reserveRepository.GetById(id);

                if (entity == null)
                {
                    return new DataResult<ReserveModel>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                ReserveModel model = _mapper.Map(entity);

                return new DataResult<ReserveModel>
                {
                    Success = true,
                    Data = model,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with getting Reserve by id : {id}");
                return new DataResult<ReserveModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> UpdateReserve(int id, ReserveModel model)
        {
            try
            {
                Reserve entity = await _reserveRepository.GetById(id);

                if (entity == null)
                {
                    return new Result { Success = false, ErrorCode = ErrorCode.NotFound, };
                }

                return await _reserveRepository.Update(_mapper.MapUpdate(entity, model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with updating Reserve by id : {id}");
                return new DataResult<ReserveModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<ReserveModel>> CreateReserve(ReserveModel model)
        {
            try
            {
                Reserve entity = _mapper.MapBack(model);

                DataResult<Reserve> result = await _reserveRepository.Add(entity);

                return new DataResult<ReserveModel>
                {
                    Success = result.Success,
                    ErrorCode = result.ErrorCode,
                    Data = _mapper.Map(result.Data),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with creating Reserve");
                return new DataResult<ReserveModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }


        }

        public async Task<Result> DeleteReserve(int id)
        {
            try
            {
                Reserve entity = await _reserveRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _reserveRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with deleting Reserve by id : {id}");
                return new DataResult<ReserveModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }
    }
}
