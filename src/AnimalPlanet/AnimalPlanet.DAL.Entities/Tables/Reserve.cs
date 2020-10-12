using System.Collections.Generic;

namespace AnimalPlanet.DAL.Entities.Tables
{
    public class Reserve
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public List<ReserveSpecie> ReserveAnimals { get; set; }
    }
}
