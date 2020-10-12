namespace AnimalPlanet.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public ErrorCode ErrorCode { get; set; }  
    }

    public class DataResult<TData> : Result
    {
        public TData Data { get; set; }
    }
}
