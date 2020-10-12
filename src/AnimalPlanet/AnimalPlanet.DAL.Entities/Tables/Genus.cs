using System.Collections.Generic;

namespace AnimalPlanet.DAL.Entities.Tables
{
    /// <summary>
    /// Род животного мира (ответвление от семейства)
    /// </summary>
    public class Genus
    {
        public int Id { get; set; }
        public string Denomination { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }

        public List<Specie> Species { get; set; }
    }
}
