using System.Collections.Generic;

namespace AnimalPlanet.Models.Models
{
    public class ClassModel
    {
        public int Id { get; set; }
        public string Denomination { get; set; }

        public int PhylumId { get; set; }
        public string PhylumDenomination { get; set; }
    }
}
