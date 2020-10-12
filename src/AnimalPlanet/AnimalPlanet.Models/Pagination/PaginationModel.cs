using System;

namespace AnimalPlanet.Models.Pagination
{
    public class PaginationModel
    {
        public int PageNumber { get; protected set; }
        public int Total { get; protected set; }
        public string Action { get; protected set; }

        private PaginationModel()
        {

        }
        public PaginationModel(int count, int pageNumber, int pageSize, string action)
        {
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            Total = (int)Math.Ceiling(count / (double)pageSize);
            Action = action;
        }

        public bool HasPrevious => (PageNumber > 1);
        public bool HasNext => (PageNumber < Total);
    }
}
