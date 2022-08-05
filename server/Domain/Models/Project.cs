using Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Project : Entity
    {
        public int ExternalID { get; set; }

        [Required]
        public string Title { get; set; }

        public bool Archived { get; set; }

        public IList<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
