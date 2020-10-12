using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPlanet.Models;

namespace AnimalPlanet.DAL.Abstract.IRepositories.Base
{
    public interface IGenericKeyRepository<TKey, TEntity>
    {
        Task<DataResult<TEntity>> Add(TEntity entity);
        Task<Result> Update(TEntity entity);
        Task<Result> Delete(TEntity entity);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(TKey id);
        Task<int> GetCount();
        Task<List<TEntity>> GetPart(int skip, int take);
        Task Save();
    }
}
