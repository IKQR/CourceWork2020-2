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
    public class NaturalAreaService : INaturalAreaService
    {
        private readonly ILogger<NaturalAreaService> _logger;
        private readonly IMapper<NaturalArea, NaturalAreaModel> _mapper;
        private readonly INaturalAreaRepository _naturalAreaRepository;

        public NaturalAreaService(
            ILogger<NaturalAreaService> logger,
            IMapper<NaturalArea, NaturalAreaModel> mapper,
            INaturalAreaRepository naturalAreaRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _naturalAreaRepository = naturalAreaRepository;
        }

        public async Task<DataResult<List<NaturalAreaModel>>> GetPartOfNaturalAreas(int skip, int take)
        {
            try
            {
                List<NaturalArea> entities = await _naturalAreaRepository.GetPart(skip, take);

                List<NaturalAreaModel> models = entities.Select(_mapper.Map).ToList();

                return new DataResult<List<NaturalAreaModel>> { Success = true, Data = models, };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problems with getting all NaturalAreas");
                return new DataResult<List<NaturalAreaModel>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<NaturalAreaModel>> GetNaturalAreaById(int id)
        {
            try
            {

                NaturalArea entity = await _naturalAreaRepository.GetById(id);

                if (entity == null)
                {
                    return new DataResult<NaturalAreaModel>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                NaturalAreaModel model = _mapper.Map(entity);

                return new DataResult<NaturalAreaModel>
                {
                    Success = true,
                    Data = model,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with getting NaturalArea by id : {id}");
                return new DataResult<NaturalAreaModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> UpdateNaturalArea(int id, NaturalAreaModel model)
        {
            try
            {
                NaturalArea entity = await _naturalAreaRepository.GetById(id);

                if (entity == null)
                {
                    return new Result { Success = false, ErrorCode = ErrorCode.NotFound, };
                }

                return await _naturalAreaRepository.Update(_mapper.MapUpdate(entity, model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with updating NaturalArea by id : {id}");
                return new DataResult<NaturalAreaModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<NaturalAreaModel>> CreateNaturalArea(NaturalAreaModel model)
        {
            try
            {
                NaturalArea entity = _mapper.MapBack(model);

                DataResult<NaturalArea> result = await _naturalAreaRepository.Add(entity);

                return new DataResult<NaturalAreaModel>
                {
                    Success = result.Success,
                    ErrorCode = result.ErrorCode,
                    Data = _mapper.Map(result.Data),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with creating NaturalArea");
                return new DataResult<NaturalAreaModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }


        }

        public async Task<Result> DeleteNaturalArea(int id)
        {
            try
            {
                NaturalArea entity = await _naturalAreaRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _naturalAreaRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with deleting NaturalArea by id : {id}");
                return new DataResult<NaturalAreaModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }
    }
}
