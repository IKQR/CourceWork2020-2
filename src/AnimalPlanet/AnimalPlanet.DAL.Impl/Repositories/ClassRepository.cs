using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.DAL.Impl.Repositories.Base;
using AnimalPlanet.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalPlanet.DAL.Impl.Repositories
{
    public class ClassRepository : GenericKeyRepository<int, Class>, IClassRepository
    {
        public ClassRepository(AnimalPlanetDbContext context) 
            : base(context)
        {

        }

        public async override Task<List<Class>> GetPart(int skip, int take)
        {
            return await DbSet
                .OrderBy(e => e.Phylum.Denomination)
                .ThenBy(e => e.Denomination)
                .Skip(skip).Take(take)
                .Include(e=>e.Phylum)
                .ToListAsync();
        }

        public async override Task<Class> GetById(int id)
        {
            return await DbSet
                .Include(e => e.Phylum)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
