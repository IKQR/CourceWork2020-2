using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.DAL.Impl.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AnimalPlanet.DAL.Impl.Repositories
{
    public class PhylumRepository : GenericKeyRepository<int, Phylum>, IPhylumRepository
    {
        public PhylumRepository(AnimalPlanetDbContext context) 
            : base(context)
        {
        }

        public async override Task<List<Phylum>> GetPart(int skip, int take)
        {
            return await DbSet.OrderBy(e=> e.Denomination).Skip(skip).Take(take).ToListAsync();
        }
    }
}
