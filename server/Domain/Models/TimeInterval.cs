using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class TimeInterval : Entity
    {
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public int WorkTypeId { get; set; }

        public WorkType WorkType { get; set; }

        public float Duration { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
