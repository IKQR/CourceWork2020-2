using System.Collections.Generic;

namespace AnimalPlanet.DAL.Entities.Tables
{
    /// <summary>
    /// Семейство животного мира (ответвление от порядка)
    /// </summary>
    public class Family
    {
        public int Id { get; set; }
        public string Denomination { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public List<Genus> Genuses { get; set; }
    }
}
