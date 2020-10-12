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
    public class FamilyService : IFamilyService
    {
        private readonly ILogger<FamilyService> _logger;
        private readonly IMapper<Family, FamilyModel> _mapper;
        private readonly IFamilyRepository _familyRepository;

        public FamilyService(
            ILogger<FamilyService> logger,
            IMapper<Family, FamilyModel> mapper,
            IFamilyRepository familyRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _familyRepository = familyRepository;
        }

        public async Task<DataResult<List<FamilyModel>>> GetPartOfFamilies(int skip, int take)
        {
            try
            {
                List<Family> entities = await _familyRepository.GetPart(skip, take);

                List<FamilyModel> models = entities.Select(_mapper.Map).ToList();

                return new DataResult<List<FamilyModel>>
                {
                    Success = true,
                    Data = models,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problems with getting all familyes");
                return new DataResult<List<FamilyModel>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<FamilyModel>> GetFamilyById(int id)
        {
            try
            {

                Family entity = await _familyRepository.GetById(id);

                if (entity == null)
                {
                    return new DataResult<FamilyModel>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                FamilyModel model = _mapper.Map(entity);

                return new DataResult<FamilyModel>
                {
                    Success = true,
                    Data = model,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with getting Family by id : {id}");
                return new DataResult<FamilyModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> UpdateFamily(int id, FamilyModel model)
        {
            try
            {
                Family entity = await _familyRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _familyRepository.Update(_mapper.MapUpdate(entity, model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with updating Family by id : {id}");
                return new DataResult<FamilyModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<FamilyModel>> CreateFamily(FamilyModel model)
        {
            try
            {
                Family entity = _mapper.MapBack(model);

                DataResult<Family> result = await _familyRepository.Add(entity);

                return new DataResult<FamilyModel>
                {
                    Success = result.Success,
                    ErrorCode = result.ErrorCode,
                    Data = _mapper.Map(result.Data),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with creating Family");
                return new DataResult<FamilyModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> DeleteFamily(int id)
        {
            try
            {
                Family entity = await _familyRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _familyRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with deleting Family by id : {id}");
                return new Result
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }
    }
}
