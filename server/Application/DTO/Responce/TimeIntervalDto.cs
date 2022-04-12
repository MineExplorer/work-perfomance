namespace Application.ViewModels
{
    using Domain.Models;
    using System;

    public class TimeIntervalDto
    {
        public TimeIntervalDto(TimeInterval timeInterval)
        {
            Id = timeInterval.Id;
            EmployeeId = timeInterval.EmployeeId;
            if (timeInterval.Employee != null)
            {
                Employee = new EmployeeDto(timeInterval.Employee);
            }
            ProjectId = timeInterval.ProjectId;
            if (timeInterval.Project != null)
            {
                Project = timeInterval.Project.Title;
            }
            WorkTypeId = timeInterval.WorkTypeId;
            if (timeInterval.WorkType != null)
            {
                WorkType = timeInterval.WorkType.Name;
            }
            Duration = timeInterval.Duration;
            Description = timeInterval.Description;
            Date = timeInterval.Date.ToShortDateString();
        }

        public TimeIntervalDto()
        {
        }

        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public EmployeeDto Employee { get; set; }

        public int ProjectId { get; set; }

        public string Project { get; set; }

        public int WorkTypeId { get; set; }

        public string WorkType { get; set; }

        public float Duration { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }
    }
}
