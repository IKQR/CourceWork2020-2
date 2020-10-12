using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Abstract.IServices
{
    public interface IGenusService
    {
        public Task<DataResult<List<GenusModel>>> GetPartOfGenuses(int skip, int take);
        public Task<DataResult<GenusModel>> GetGenusById(int id);
        public Task<Result> UpdateGenus(int id, GenusModel model);
        public Task<DataResult<GenusModel>> CreateGenus(GenusModel model);
        public Task<Result> DeleteGenus(int id);
    }
}
