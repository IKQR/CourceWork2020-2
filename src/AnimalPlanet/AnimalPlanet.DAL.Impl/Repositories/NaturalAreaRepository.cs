using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.DAL.Impl.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace AnimalPlanet.DAL.Impl.Repositories
{
    public class NaturalAreaRepository : GenericKeyRepository<int, NaturalArea>, INaturalAreaRepository
    {
        public NaturalAreaRepository(AnimalPlanetDbContext context)
            : base(context)
        {
        }

        public async override Task<List<NaturalArea>> GetPart(int skip, int take)
        {
            return await DbSet.OrderBy(e => e.Denomination).Skip(skip).Take(take).ToListAsync();
        }
    }
}
