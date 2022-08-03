using System.Collections.Generic;
using System.Linq;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Models;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class TaskService: ITaskService
    {
        private TaskRepository _employeeTaskRepository;

        public TaskService(TaskRepository employeeRepository)
        {
            _employeeTaskRepository = employeeRepository;
        }

        public TaskDto GetTask(int id)
        {
            Task result = _employeeTaskRepository.GetTask(id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }
            return new TaskDto(result);
        }

        public List<TaskDto> GetTasksForEmployee(int employeeId)
        {
            return _employeeTaskRepository.GetTasksForEmployee(employeeId).Select(e => new TaskDto(e)).ToList();
        }

        public TaskDto InsertTask(TaskCreateRequestDto task)
        {
            return new TaskDto(_employeeTaskRepository.InsertTask(task.ToModel()));
        }

        public void DeleteTask(int id)
        {
            _employeeTaskRepository.DeleteTask(id);
        }
    }
}
