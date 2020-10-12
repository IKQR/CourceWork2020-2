using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPlanet.DAL.Abstract.IRepositories.Base;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.DAL.Abstract.IRepositories
{
    public interface ISpecieRepository : IGenericKeyRepository<int, Specie>
    {
        public Task<SpecieViewModel> GetModelById(int id);
        public Task<List<SpecieViewModel>> GetModelsPart(int skip, int take);
    }
}
