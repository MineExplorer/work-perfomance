namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.ViewModels;
    using Domain.Enums;
    using Domain.Models;
    using Infrastructure.Repositories;

    public class EmployeeFeedbackService
    {
        private EmployeeFeedbackRepository _employeeFeedbackRepository;

        public EmployeeFeedbackService(EmployeeFeedbackRepository employeeRepository)
        {
            _employeeFeedbackRepository = employeeRepository;
        }

        public EmployeeFeedbackDto InsertFeedback(EmployeeFeedbackCreateRequestDto feedback)
        {
            return new EmployeeFeedbackDto(_employeeFeedbackRepository.InsertFeedback(feedback.ToModel()));
        }

        public List<EmployeeFeedbackDto> GetFeedbackForEmployee(int employeeId)
        {
            return _employeeFeedbackRepository.GetFeedbackForEmployee(employeeId).Select(e => new EmployeeFeedbackDto(e)).ToList();
        }

        public void DeleteFeedback(int id)
        {
            _employeeFeedbackRepository.DeleteFeedback(id);
        }
    }
}
