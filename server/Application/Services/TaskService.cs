using System.Collections.Generic;
using System.Linq;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class TaskService: ITaskService
    {
        private ITaskRepository _employeeTaskRepository;

        public TaskService(ITaskRepository employeeRepository)
        {
            _employeeTaskRepository = employeeRepository;
        }

        public TaskDto GetTask(int id)
        {
            TaskEntity result = _employeeTaskRepository.Get(id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }
            return new TaskDto(result);
        }

        public List<TaskDto> GetTasksForEmployee(int employeeId)
        {
            return _employeeTaskRepository.GetAllForEmployee(employeeId).Select(e => new TaskDto(e)).ToList();
        }

        public TaskDto InsertTask(TaskCreateRequestDto task)
        {
            return new TaskDto(_employeeTaskRepository.Create(task.ToModel()));
        }

        public void DeleteTask(int id)
        {
            _employeeTaskRepository.Delete(id);
        }
    }
}
