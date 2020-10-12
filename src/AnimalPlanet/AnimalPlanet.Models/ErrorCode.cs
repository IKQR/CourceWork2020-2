using System.ComponentModel;

namespace AnimalPlanet.Models
{
    public enum ErrorCode
    {
        None,
        [Description("Internal error was accured")]
        InternalError,
        [Description("Data not found")]
        NotFound,
        [Description("Such a record already exists")]
        UniquenessError,
        [Description("The insert or update value of foreign key is invalid")]
        ForeignKeyViolation,
    }
}
