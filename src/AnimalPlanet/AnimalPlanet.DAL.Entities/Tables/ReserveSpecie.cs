using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalPlanet.DAL.Entities.Tables
{
    public class ReserveSpecie
    {
        public int ReserveId { get; set; }
        public Reserve Reserve { get; set; }

        public int SpecieId { get; set; }
        public Specie Specie { get; set; }
    }
}
