namespace AnimalPlanet.Models.Models
{
    public class SpecieCreateModel
    {
        public string Denomination { get; set; }
        public string Description { get; set; }

        public int GenusId { get; set; }

        public int[] NaturalAreas { get; set; }
        public int[] Reserves { get; set; }
    }
}
