using System;
using Domain.Models;

namespace Application.ViewModels
{
    public class TaskDto
    {
        public TaskDto(TaskEntity task)
        {
            Id = task.Id;
            EmployeeId = task.EmployeeId;
            if (task.Employee != null) {
                Employee = new EmployeeDto(task.Employee);
            }
            Title = task.Title;
            Description = task.Description;
            EstimateTime = task.EstimateTime;
            SpentTime = task.SpentTime;
        }

        public TaskDto()
        {
        }

        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public EmployeeDto Employee { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public float EstimateTime { get; set; }

        public float SpentTime { get; set; }
    }
}
