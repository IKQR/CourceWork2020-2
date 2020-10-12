using System.Collections.Generic;
using System.Threading.Tasks;

using AnimalPlanet.Models;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Abstract.IServices
{
    public interface IReserveService
    {
        public Task<DataResult<List<ReserveModel>>> GetPartOfReserves(int skip, int take);
        public Task<DataResult<ReserveModel>> GetReserveById(int id);
        public Task<Result> UpdateReserve(int id, ReserveModel model);
        public Task<DataResult<ReserveModel>> CreateReserve(ReserveModel model);
        public Task<Result> DeleteReserve(int id);
    }
}
