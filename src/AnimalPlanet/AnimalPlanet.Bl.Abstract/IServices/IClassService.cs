using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Abstract.IServices
{
    public interface IClassService
    {
        public Task<DataResult<List<ClassModel>>> GetPartOfClasses(int skip, int take);
        public Task<DataResult<ClassModel>> GetClassById(int id);
        public Task<Result> UpdateClass(int id, ClassModel model);
        public Task<DataResult<ClassModel>> CreateClass(ClassModel model);
        public Task<Result> DeleteClass(int id);
    }
}
