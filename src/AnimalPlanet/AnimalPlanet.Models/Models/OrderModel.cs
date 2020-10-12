namespace AnimalPlanet.Models.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Denomination { get; set; }

        public int ClassId { get; set; }
        public string ClassDenomination { get; set; }
    }
}
