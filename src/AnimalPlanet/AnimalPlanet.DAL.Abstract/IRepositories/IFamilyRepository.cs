using AnimalPlanet.DAL.Abstract.IRepositories.Base;
using AnimalPlanet.DAL.Entities.Tables;

namespace AnimalPlanet.DAL.Abstract.IRepositories
{
    public interface IFamilyRepository : IGenericKeyRepository<int, Family>
    {
    }
}
