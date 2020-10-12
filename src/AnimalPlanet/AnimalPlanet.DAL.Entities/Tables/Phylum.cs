using System.Collections.Generic;

namespace AnimalPlanet.DAL.Entities.Tables
{
    /// <summary>
    /// Отдел животного мира (ответвление от царства)
    /// </summary>
    public class Phylum
    {
        public int Id { get; set; }
        public string Denomination { get; set; }

        public List<Class> Classes { get; set; }
    }
}
