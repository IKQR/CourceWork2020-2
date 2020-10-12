namespace AnimalPlanet.Models.Models
{
    public class GenusModel
    {
        public int Id { get; set; }
        public string Denomination { get; set; }

        public int FamilyId { get; set; }
        public string FamilyDenomination { get; set; }
    }
}
