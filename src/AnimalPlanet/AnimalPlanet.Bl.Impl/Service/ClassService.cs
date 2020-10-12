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
    public class ClassService : IClassService
    {
        private readonly ILogger<ClassService> _logger;
        private readonly IMapper<Class, ClassModel> _mapper;
        private readonly IClassRepository _classRepository;

        public ClassService(
            ILogger<ClassService> logger,
            IMapper<Class, ClassModel> mapper,
            IClassRepository classRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _classRepository = classRepository;
        }

        public async Task<DataResult<List<ClassModel>>> GetPartOfClasses(int skip, int take)
        {
            try
            {
                List<Class> entities = await _classRepository.GetPart(skip, take);

                List<ClassModel> models = entities.Select(_mapper.Map).ToList();

                return new DataResult<List<ClassModel>>
                {
                    Success = true, 
                    Data = models,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problems with getting all classes");
                return new DataResult<List<ClassModel>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<ClassModel>> GetClassById(int id)
        {
            try
            {

                Class entity = await _classRepository.GetById(id);

                if (entity == null)
                {
                    return new DataResult<ClassModel>
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                ClassModel model = _mapper.Map(entity);

                return new DataResult<ClassModel>
                {
                    Success = true,
                    Data = model,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with getting Class by id : {id}");
                return new DataResult<ClassModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> UpdateClass(int id, ClassModel model)
        {
            try
            {
                Class entity = await _classRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false, 
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _classRepository.Update(_mapper.MapUpdate(entity, model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with updating Class by id : {id}");
                return new DataResult<ClassModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<ClassModel>> CreateClass(ClassModel model)
        {
            try
            {
                Class entity = _mapper.MapBack(model);

                DataResult<Class> result = await _classRepository.Add(entity);

                return new DataResult<ClassModel>
                {
                    Success = result.Success,
                    ErrorCode = result.ErrorCode,
                    Data = _mapper.Map(result.Data),
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with creating Class");
                return new DataResult<ClassModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> DeleteClass(int id)
        {
            try
            {
                Class entity = await _classRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _classRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with deleting Class by id : {id}");
                return new DataResult<PhylumModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }
    }
}
