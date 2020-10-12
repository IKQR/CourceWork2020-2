using System;

using Microsoft.Extensions.Configuration;

namespace AnimalPlanet.Configuration
{
    public class PaginationConfiguration
    {
        private readonly IConfiguration _configuration;

        public PaginationConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int Width => Int32.Parse(_configuration["Pagination:Width"]);
        public int PageSize => Int32.Parse(_configuration["Pagination:PageSize"]);
    }
}
