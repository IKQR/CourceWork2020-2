using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Abstract.IServices
{
    public interface INaturalAreaService
    {
        public Task<DataResult<List<NaturalAreaModel>>> GetPartOfNaturalAreas(int skip, int take);
        public Task<DataResult<NaturalAreaModel>> GetNaturalAreaById(int id);
        public Task<Result> UpdateNaturalArea(int id, NaturalAreaModel model);
        public Task<DataResult<NaturalAreaModel>> CreateNaturalArea(NaturalAreaModel model);
        public Task<Result> DeleteNaturalArea(int id);
    }
}
