using System;
using System.Linq;
using Domain.Models;

namespace Application.ViewModels
{
    public class EmployeeDto
    {
        public EmployeeDto(Employee employee, bool serializeProjects = false)
        {
            Id = employee.Id;
            Email = employee.Email;
            FullName = employee.Name;
            Password = employee.Password;
            Seniority = (int)employee.Seniority;
            Experience = employee.Experience;
            HourlyRate = employee.HourlyRate;
            PermissionLevel = (int)employee.PermissionLevel;
            WorkDayDuration = employee.WorkDayDuration;
            Created = employee.Created;
            if (serializeProjects && employee.ProjectEmployees != null)
            {
                Projects = employee.ProjectEmployees.Select(e => new ProjectDto(e.Project, false)).ToArray();
            }
        }

        public EmployeeDto()
        {
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public int PermissionLevel { get; set; }

        public int Seniority { get; set; }

        public int Experience { get; set; }

        public float HourlyRate { get; set; }

        public float WorkDayDuration { get; set; }

        public DateTime Created { get; set; }

        public ProjectDto[] Projects { get; set; }
    }
}
