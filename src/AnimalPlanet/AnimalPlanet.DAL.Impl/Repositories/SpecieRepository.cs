using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalPlanet.DAL.Abstract.IRepositories;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.DAL.Impl.Repositories.Base;
using AnimalPlanet.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalPlanet.DAL.Impl.Repositories
{
    public class SpecieRepository : GenericKeyRepository<int, Specie> , ISpecieRepository
    {
        public SpecieRepository(AnimalPlanetDbContext context) 
            : base(context)
        {

        }

        public async override Task<Specie> GetById(int id)
        {
            return await DbSet
                .OrderBy(m => m.Denomination)
                .ThenBy(m => m.Genus.Denomination)
                .Include(m => m.NaturalAreaSpecies)
                .Include(m => m.ReserveSpecies)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<SpecieViewModel> GetModelById(int id)
        {
            Specie specie = await DbSet
                .Include(e => e.Genus)
                .Include(m => m.NaturalAreaSpecies)
                .Include(m => m.ReserveSpecies)
                .FirstOrDefaultAsync(e => e.Id == id);

            List<Reserve> reserves = await Context.Reserves
                .OrderBy(m => m.Name)
                .ToListAsync();

            List<NaturalArea> naturalAreas = await Context.NaturalAreas
                .OrderBy(m => m.Denomination)
                .ToListAsync();

            return new SpecieViewModel
            {
                Id = specie.Id,
                Denomination = specie.Denomination,
                Description = specie.Description,
                GenusDenomination = specie.Genus.Denomination,
                NaturalAreas = naturalAreas
                    .Where(m => specie.NaturalAreaSpecies.Any(x => x.NaturalAreaId == m.Id))
                    .Select(m => m.Denomination)
                    .ToList(),
                Reserves = reserves
                    .Where(m => specie.ReserveSpecies.Any(x => x.ReserveId == m.Id))
                    .Select(m => m.Name)
                    .ToList(),
            };
        }

        public async Task<List<SpecieViewModel>> GetModelsPart(int skip, int take)
        {
           List<Specie> species = await DbSet
               .OrderBy(e => e.Genus.Denomination)
               .ThenBy(e => e.Denomination)
               .Skip(skip).Take(take)
               .Include( m => m.NaturalAreaSpecies)
               .Include( m => m.ReserveSpecies)
               .Include( m => m.Genus)
               .ToListAsync();

            return species
                .Select(s => new SpecieViewModel
                {
                    Id = s.Id,
                    Denomination = s.Denomination,
                    Description = s.Description,
                    GenusDenomination = s.Genus.Denomination,
                }).ToList();

            //List<Reserve> reserves = await Context.Reserves
            //    .OrderBy(m => m.Name)
            //    .ToListAsync();

            //List<NaturalArea> naturalAreas =await Context.NaturalAreas
            //    .OrderBy(m => m.Denomination)
            //    .ToListAsync();

            //return species
            //    .Select(s => new SpecieViewModel
            //    {
            //        Id = s.Id,
            //        Denomination = s.Denomination,
            //        Description = s.Description,
            //        NaturalAreas = naturalAreas
            //            .Where(m => s.NaturalAreaSpecies.Any(x => x.NaturalAreaId == m.Id))
            //            .Select(m=>m.Denomination)
            //            .ToList(),
            //        Reserves = reserves
            //            .Where(m => s.ReserveSpecies.Any(x => x.ReserveId == m.Id))
            //            .Select(m => m.Name)
            //            .ToList(),
            //    }).ToList();
        }
    }
}
