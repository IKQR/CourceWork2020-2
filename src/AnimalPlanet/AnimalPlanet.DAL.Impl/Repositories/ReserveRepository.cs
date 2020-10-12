using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.DAL.Impl.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace AnimalPlanet.DAL.Impl.Repositories
{
    public class ReserveRepository : GenericKeyRepository<int, Reserve>, IReserveRepository
    {
        public ReserveRepository(AnimalPlanetDbContext context)
            : base(context)
        {

        }

        public async override Task<List<Reserve>> GetPart(int skip, int take)
        {
            return await DbSet.OrderBy(e => e.Name).Skip(skip).Take(take).ToListAsync();
        }
    }
}

