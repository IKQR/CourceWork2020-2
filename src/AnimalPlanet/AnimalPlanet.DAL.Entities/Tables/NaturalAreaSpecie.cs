namespace AnimalPlanet.DAL.Entities.Tables
{
    public class NaturalAreaSpecie
    {
        public int NaturalAreaId { get; set; }
        public NaturalArea NaturalArea { get; set; }

        public int SpecieId { get; set; }
        public Specie Specie { get; set; }
    }
}
