using System.Collections.Generic;

namespace AnimalPlanet.Models.Pagination
{
    public class GenericPaginatedModel<TModel>
    {
        public IEnumerable<TModel> Models { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
