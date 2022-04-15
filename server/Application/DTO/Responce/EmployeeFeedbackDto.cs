using System;
using Domain.Models;

namespace Application.ViewModels
{
    public class EmployeeFeedbackDto
    {
        public EmployeeFeedbackDto(EmployeeFeedback feedback)
        {
            Id = feedback.Id;
            EmployeeId = feedback.EmployeeId;
            ClientAssesment = feedback.ClientAssesment;
            PerfomanceAssesment = feedback.PerfomanceAssesment;
            CodeQualityAssesment = feedback.CodeQualityAssesment;
            TeamworkAssesment = feedback.TeamworkAssesment;
            Created = feedback.Created;
        }

        public EmployeeFeedbackDto()
        {
        }

        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public float ClientAssesment { get; set; }

        public float PerfomanceAssesment { get; set; }

        public float CodeQualityAssesment { get; set; }

        public float TeamworkAssesment { get; set; }

        public DateTime Created { get; set; }
    }
}
