using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Abstract.IServices
{
    public interface ISpecieService
    {
        public Task<DataResult<List<SpecieViewModel>>> GetPartOfSpecieViews(int skip, int take);
        public Task<DataResult<SpecieViewModel>> GetSpecieViewById(int id);
        public Task<DataResult<SpecieCreateModel>> GetSpecieById(int id);
        public Task<Result> UpdateSpecie(int id, SpecieCreateModel model);
        public Task<DataResult<Specie>> CreateSpecie(SpecieCreateModel model);
        public Task<Result> DeleteSpecie(int id);
    }
}
