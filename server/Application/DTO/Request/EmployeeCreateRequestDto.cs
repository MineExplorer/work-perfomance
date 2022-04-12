using Application.Interfaces;
using Domain.Enums;
using Domain.Models;

namespace Application.DTO.Request
{
    public class EmployeeCreateRequestDto : IDtoMapper<Employee>
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public int PermissionLevel { get; set; }

        public int Seniority { get; set; }

        public int Experience { get; set; }

        public string TechStack { get; set; }

        public Employee ToModel()
        {
            return new Employee()
            {
                Email = this.Email,
                Name = this.FullName,
                Password = this.Password,
                Seniority = (Seniority) this.Seniority,
                Experience = this.Experience,
                TechStack = this.TechStack,
                PermissionLevel = (PermissionLevel) this.PermissionLevel
            };
            
        }
    }
}
