using System.Linq;
using Domain.Models;

namespace Application.ViewModels
{
    public class ProjectDto
    {
        public ProjectDto(Project project, bool serializeEmployees = false)
        {
            Id = project.Id;
            Title = project.Title;
            if (serializeEmployees && project.ProjectEmployees != null)
            {
                Employees = project.ProjectEmployees.Select(e => new EmployeeDto(e.Employee, false)).ToArray();
            }
        }

        public ProjectDto()
        {
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public EmployeeDto[] Employees { get; set; }
    }
}
