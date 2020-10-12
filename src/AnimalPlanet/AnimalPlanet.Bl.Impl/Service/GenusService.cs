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
    public class GenusService : IGenusService
    {
        private readonly ILogger<GenusService> _logger;
        private readonly IMapper<Genus, GenusModel> _mapper;
        private readonly IGenusRepository _genusRepository;

        public GenusService(
            ILogger<GenusService> logger,
            IMapper<Genus, GenusModel> mapper,
            IGenusRepository genusRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _genusRepository = genusRepository;
        }

        public async Task<DataResult<List<GenusModel>>> GetPartOfGenuses(int skip, int take)
        {
            try
            {
                List<Genus> entities = await _genusRepository.GetPart(skip, take);

                List<GenusModel> models = entities.Select(_mapper.Map).ToList();

                return new DataResult<List<GenusModel>>
                {
                    Success = true,
                    Data = models,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problems with getting all genuses");
                return new DataResult<List<GenusModel>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<GenusModel>> GetGenusById(int id)
        {
            try
            {

                Genus entity = await _genusRepository.GetById(id);

                if (entity == null)
                {
                    return new DataResult<GenusModel>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                GenusModel model = _mapper.Map(entity);

                return new DataResult<GenusModel>
                {
                    Success = true,
                    Data = model,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with getting Genus by id : {id}");
                return new DataResult<GenusModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> UpdateGenus(int id, GenusModel model)
        {
            try
            {
                Genus entity = await _genusRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _genusRepository.Update(_mapper.MapUpdate(entity, model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with updating Genus by id : {id}");
                return new DataResult<GenusModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<GenusModel>> CreateGenus(GenusModel model)
        {
            try
            {
                Genus entity = _mapper.MapBack(model);

                DataResult<Genus> result = await _genusRepository.Add(entity);

                return new DataResult<GenusModel>
                {
                    Success = result.Success,
                    ErrorCode = result.ErrorCode,
                    Data = _mapper.Map(result.Data),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with creating Genus");
                return new DataResult<GenusModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> DeleteGenus(int id)
        {
            try
            {
                Genus entity = await _genusRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _genusRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with deleting Genus by id : {id}");
                return new Result
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }
    }
}
