using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.DAL.Impl.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace AnimalPlanet.DAL.Impl.Repositories
{
    public class GenusRepository : GenericKeyRepository<int, Genus>, IGenusRepository
    {
        public GenusRepository(AnimalPlanetDbContext context)
            : base(context)
        {

        }

        public async override Task<List<Genus>> GetPart(int skip, int take)
        {
            return await DbSet
                .OrderBy(e => e.Family.Denomination)
                .ThenBy(e => e.Denomination)
                .Skip(skip).Take(take)
                .Include(e => e.Family)
                .ToListAsync();
        }

        public async override Task<Genus> GetById(int id)
        {
            return await DbSet
                .Include(e => e.Family)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
