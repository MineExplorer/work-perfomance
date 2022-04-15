using System;
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
            TechStack = employee.TechStack;
            PermissionLevel = (int)employee.PermissionLevel;
            Created = employee.Created;
            if (employee.ProjectEmployees != null)
            {
                Projects = employee.ProjectEmployees.Select(e => new ProjectDto(e.Project)).ToArray();
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

        public string TechStack { get; set; }

        public DateTime Created { get; set; }

        public ProjectDto[] Projects { get; set; }
    }
}
