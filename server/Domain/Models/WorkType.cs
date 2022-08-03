using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class WorkType : Entity
    {
        [Required]
        public string Name { get; set; }
    }
}
