using System;
using System.Collections.Generic;
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
    public class SpecieService : ISpecieService
    {
        private readonly ILogger<SpecieService> _logger;
        private readonly IMapper<Specie, SpecieCreateModel> _mapper;
        private readonly ISpecieRepository _specieRepository;

        public SpecieService(
            ILogger<SpecieService> logger,
            IMapper<Specie, SpecieCreateModel> mapper,
            ISpecieRepository specieRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _specieRepository = specieRepository;
        }

        public async Task<DataResult<List<SpecieViewModel>>> GetPartOfSpecieViews(int skip, int take)
        {
            try
            {
                List<SpecieViewModel> models = await _specieRepository.GetModelsPart(skip, take);

                return new DataResult<List<SpecieViewModel>>
                {
                    Success = true,
                    Data = models,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with getting Species");
                return new DataResult<List<SpecieViewModel>>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<SpecieViewModel>> GetSpecieViewById(int id)
        {
            try
            {
                SpecieViewModel model = await _specieRepository.GetModelById(id);

                return new DataResult<SpecieViewModel>
                {
                    Success = true,
                    Data = model,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with getting Specie by id : {id}");
                return new DataResult<SpecieViewModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<SpecieCreateModel>> GetSpecieById(int id)
        {
            try
            {
                Specie entity = await _specieRepository.GetById(id);

                return new DataResult<SpecieCreateModel>
                {
                    Success = true,
                    Data = _mapper.Map(entity),
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with getting Specie by id : {id}");
                return new DataResult<SpecieCreateModel>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> UpdateSpecie(int id, SpecieCreateModel model)
        {
            try
            {
                Specie entity = await _specieRepository.GetById(id);

                _mapper.MapUpdate(entity, model);

                return await _specieRepository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with updating Specie by id : {id}");
                return new Result
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<DataResult<Specie>> CreateSpecie(SpecieCreateModel model)
        {
            try
            {
                DataResult<Specie> dataResult = await _specieRepository.Add(_mapper.MapBack(model));

                Result result = await _specieRepository
                    .Update(_mapper.MapUpdate(dataResult.Data, model));
                return dataResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with creating Specie");
                return new DataResult<Specie>
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }

        public async Task<Result> DeleteSpecie(int id)
        {
            try
            {
                Specie entity = await _specieRepository.GetById(id);

                if (entity == null)
                {
                    return new Result
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NotFound,
                    };
                }

                return await _specieRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problems with deleting Specie by id : {id}");
                return new Result
                {
                    Success = false,
                    ErrorCode = ErrorCode.InternalError,
                };
            }
        }
    }
}
