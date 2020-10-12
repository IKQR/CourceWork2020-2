using System.Collections.Generic;

namespace AnimalPlanet.Models.Models
{
    public class SpecieViewModel
    {
        public int Id { get; set; }
        public string Denomination { get; set; }
        public string Description { get; set; }

        public string GenusDenomination { get; set; }

        public List<string> NaturalAreas { get; set; }
        public List<string> Reserves { get; set; }
    }
}
