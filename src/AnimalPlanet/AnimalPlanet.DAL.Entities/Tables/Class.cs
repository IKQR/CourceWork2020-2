using System.Collections.Generic;

namespace AnimalPlanet.DAL.Entities.Tables
{
    /// <summary>
    /// Класс животного мира (ответвление от отдела)
    /// </summary>
    public class Class
    {
        public int Id { get; set; }
        public string Denomination { get; set; }

        public int PhylumId { get; set; }
        public Phylum Phylum { get; set; }

        public List<Order> Orders { get; set; }
    }
}
