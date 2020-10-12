using System.Collections.Generic;

namespace AnimalPlanet.DAL.Entities.Tables
{
    public class NaturalArea
    {
        public int Id { get; set; }
        public string Denomination { get; set; }

        public List<NaturalAreaSpecie> NaturalAreaAnimals { get; set; }
    }
}
