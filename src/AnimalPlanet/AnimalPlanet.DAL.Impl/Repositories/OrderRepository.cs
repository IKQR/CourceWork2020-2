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
    public class OrderRepository : GenericKeyRepository<int, Order>, IOrderRepository
    {
        public OrderRepository(AnimalPlanetDbContext context)
            : base(context)
        {

        }

        public async override Task<List<Order>> GetPart(int skip, int take)
        {
            return await DbSet
                .OrderBy(e => e.Class.Denomination)
                .ThenBy(e => e.Denomination)
                .Skip(skip).Take(take)
                .Include(e => e.Class)
                .ToListAsync();
        }

        public async override Task<Order> GetById(int id)
        {
            return await DbSet
                .Include(e => e.Class)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
