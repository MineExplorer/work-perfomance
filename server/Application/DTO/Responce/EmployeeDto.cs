using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Application.ViewModels
{
    public class EmployeeDto
    {
        public EmployeeDto(Employee employee)
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

        public EmployeeDto MapProjects(IList<ProjectEmployee> projects)
        {
            if (projects == null)
            {
                Projects = Array.Empty<ProjectDto>();
            }
            else
            {
                Projects = projects.Select(e => new ProjectDto(e.Project)).ToArray();
            }
            return this;
        }
    }
}
