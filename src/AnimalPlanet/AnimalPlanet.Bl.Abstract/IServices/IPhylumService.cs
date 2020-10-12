using System.Collections.Generic;
using System.Threading.Tasks;

using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Abstract.IServices
{
    public interface IPhylumService
    {
        public Task<DataResult<List<PhylumModel>>> GetPartOfPhylums(int skip, int take);
        public Task<DataResult<PhylumModel>> GetPhylumById(int id);
        public Task<Result> UpdatePhylum(int id, PhylumModel model);
        public Task<DataResult<PhylumModel>> CreatePhylum(PhylumModel model);
        public Task<Result> DeletePhylum(int id);
    }
}
