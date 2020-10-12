using System.Collections.Generic;

namespace AnimalPlanet.DAL.Entities.Tables
{
    /// <summary>
    /// Вид животного мира (ответвление от рода)
    /// </summary>
    public class Specie
    {
        public int Id { get; set; }
        public string Denomination { get; set; }
        public string Description { get; set; }

        public int GenusId { get; set; }
        public Genus Genus { get; set; }

        public List<NaturalAreaSpecie> NaturalAreaSpecies { get; set; }
        public List<ReserveSpecie> ReserveSpecies { get; set; }
    }
}
