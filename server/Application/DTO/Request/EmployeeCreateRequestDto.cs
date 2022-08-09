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

        public float HourlyRate { get; set; }

        public float WorkDayDuration { get; set; } = 8;

        public Employee ToModel()
        {
            return new Employee()
            {
                Email = this.Email,
                Name = this.FullName,
                Password = this.Password,
                Seniority = (Seniority) this.Seniority,
                Experience = this.Experience,
                HourlyRate = this.HourlyRate,
                PermissionLevel = (PermissionLevel) this.PermissionLevel,
                WorkDayDuration = this.WorkDayDuration
            };
            
        }
    }
}
