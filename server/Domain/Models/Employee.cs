namespace Domain.Models
{
    using Domain.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        public PermissionLevel PermissionLevel { get; set; }

        public Seniority Seniority { get; set; }
        
        public int Experience { get; set; }

        public string TechStack { get; set; }

        public float WorkDayDuration { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public IList<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
