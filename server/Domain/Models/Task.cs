using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Task : Entity
    {
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public float EstimateTime { get; set; }

        public float SpentTime { get; set; }
    }
}
