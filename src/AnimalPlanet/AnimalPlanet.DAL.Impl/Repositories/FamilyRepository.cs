using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.DAL.Impl.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AnimalPlanet.DAL.Impl.Repositories
{
    public class FamilyRepository : GenericKeyRepository<int, Family>, IFamilyRepository
    {
        public FamilyRepository(AnimalPlanetDbContext context)
            : base(context)
        {

        }

        public async override Task<List<Family>> GetPart(int skip, int take)
        {
            return await DbSet
                .OrderBy(e => e.Order.Denomination)
                .ThenBy(e => e.Denomination)
                .Skip(skip).Take(take)
                .Include(e => e.Order)
                .ToListAsync();
        }

        public async override Task<Family> GetById(int id)
        {
            return await DbSet
                .Include(e => e.Order)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
