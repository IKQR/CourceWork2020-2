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
    public class PhylumService : IPhylumService
    {
        private readonly ILogger<PhylumService> _logger;
        private readonly IMapper<Phylum, PhylumModel> _mapper;
        private readonly IPhylumRepository _phylumRepository;

        public PhylumService(
            ILogger<PhylumService> logger,
            IMapper<Phylum, PhylumModel> mapper,
            IPhylumRepository phylumRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _phylumRepository = phylumRepository;
        }

        public async Task<DataResult<List<PhylumModel>>> GetPartOfPhylums(int skip, int take)
        {
            try
            {
                List<Phylum> entities = await _phylumRepository.GetPart(skip, take);

                List<PhylumModel> models = entities.Select(_mapper.Map).ToList();

                return new DataResult<List<PhylumModel>> { Success = true, Data = models, };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problems with getting all Phylums");
                return new DataResult<List<PhylumModel>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<PhylumModel>> GetPhylumById(int id)
        {
            try
            {

                Phylum entity = await _phylumRepository.GetById(id);

                if (entity == null)
                {
                    return new DataResult<PhylumModel>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                PhylumModel model = _mapper.Map(entity);

                return new DataResult<PhylumModel>
                {
                    Success = true,
                    Data = model,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with getting Phylum by id : {id}");
                return new DataResult<PhylumModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> UpdatePhylum(int id, PhylumModel model)
        {
            try
            {
                Phylum entity = await _phylumRepository.GetById(id);

                if (entity == null)
                {
                    return new Result { Success = false, ErrorCode = ErrorCode.NotFound, };
                }

                return await _phylumRepository.Update(_mapper.MapUpdate(entity, model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with updating Phylum by id : {id}");
                return new DataResult<PhylumModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<PhylumModel>> CreatePhylum(PhylumModel model)
        {
            try
            {
                Phylum entity = _mapper.MapBack(model);

                DataResult<Phylum> result = await _phylumRepository.Add(entity);

                return new DataResult<PhylumModel>
                {
                    Success = result.Success,
                    ErrorCode = result.ErrorCode,
                    Data = _mapper.Map(result.Data),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with creating Phylum");
                return new DataResult<PhylumModel>
                {
                    Success = false, 
                    ErrorCode = ErrorCode.InternalError,
                };
            }


        }

        public async Task<Result> DeletePhylum(int id)
        {
            try
            {
                Phylum entity = await _phylumRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false, 
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _phylumRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with deleting Phylum by id : {id}");
                return new DataResult<PhylumModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }
    }
}
