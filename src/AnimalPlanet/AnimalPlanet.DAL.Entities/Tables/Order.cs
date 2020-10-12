using System.Collections.Generic;

namespace AnimalPlanet.DAL.Entities.Tables
{
    /// <summary>
    /// Порядок животного мира (ответвление от класса)
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        public string Denomination { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public List<Family> Families { get; set; }
    }
}
