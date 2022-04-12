using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class EmployeeAssessment
    {
        public int Id { get; set; }

        public float ClientAssesment { get; set; }

        public float PerfomanceAssesment { get; set; }

        public float CodeQualityAssesment { get; set; }

        public float TeamworkAssesment { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
