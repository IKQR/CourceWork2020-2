using System;
using System.Collections.Generic;
using System.Linq;

using AnimalPlanet.Bl.Abstract.Mappers;
using AnimalPlanet.DAL.Entities.Tables;
using AnimalPlanet.Models.Models;

namespace AnimalPlanet.Bl.Impl.Mappers
{
    public class SpecieCreateMapper : IMapper<Specie, SpecieCreateModel>
    {
        public SpecieCreateModel Map(Specie entity)
        {
            return new SpecieCreateModel
            {
                Denomination = entity.Denomination,
                NaturalAreas = entity.NaturalAreaSpecies
                    .Select(x => x.NaturalAreaId)
                    .ToArray(),
                Reserves = entity.ReserveSpecies
                    .Select(x => x.ReserveId)
                    .ToArray(),
                GenusId = entity.GenusId,
                Description = entity.Description,
            };
        }

        public Specie MapBack(SpecieCreateModel model)
        {
            return new Specie
            {
                Denomination = model.Denomination,
                Description = model.Description,
                GenusId = model.GenusId,
            };
        }

        public Specie MapUpdate(Specie entity, SpecieCreateModel model)
        {
            entity.Denomination = model.Denomination;
            entity.Description = model.Description;
            entity.GenusId = model.GenusId;
            entity.NaturalAreaSpecies = model.NaturalAreas
                .Select(x => new NaturalAreaSpecie
                {
                    SpecieId = entity.Id,
                    NaturalAreaId = x
                }).ToList();
            entity.ReserveSpecies = model.Reserves
                .Select(x => new ReserveSpecie
                {
                    SpecieId = entity.Id,
                    ReserveId = x
                }).ToList();

            return entity;
        }
    }
}
