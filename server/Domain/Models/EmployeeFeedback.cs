using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class EmployeeFeedback
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public float ClientAssesment { get; set; }

        public float PerfomanceAssesment { get; set; }

        public float CodeQualityAssesment { get; set; }

        public float TeamworkAssesment { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
