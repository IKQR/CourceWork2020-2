using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Abstract.IServices
{
    public interface IFamilyService
    {
        public Task<DataResult<List<FamilyModel>>> GetPartOfFamilies(int skip, int take);
        public Task<DataResult<FamilyModel>> GetFamilyById(int id);
        public Task<Result> UpdateFamily(int id, FamilyModel model);
        public Task<DataResult<FamilyModel>> CreateFamily(FamilyModel model);
        public Task<Result> DeleteFamily(int id);
    }
}
