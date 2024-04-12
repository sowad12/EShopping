using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Library.Model.Entities
{
    [Table(nameof(SeederStatus))]
    public class SeederStatus : BaseEntity
    {
        public string? Status { get; set; }
        public string? Exception { get; set; }
        public string? StackTrace { get; set; }
    }
}
