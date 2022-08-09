using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Application.ViewModels
{
    public class ProjectDto
    {
        public ProjectDto(Project project)
        {
            Id = project.Id;
            Title = project.Title;
            Archived = project.Archived;
        }

        public ProjectDto()
        {
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public bool Archived { get; set; }

        public EmployeeDto[] Employees { get; set; }

        public ProjectDto MapEmployees(IList<ProjectEmployee> employees)
        {
            if (employees == null)
            {
                Employees = Array.Empty<EmployeeDto>();
            }
            else
            {
                Employees = employees.Select(e => new EmployeeDto(e.Employee)).ToArray();
            }
            return this;
        }
    }
}
