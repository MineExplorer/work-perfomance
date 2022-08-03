using System.Collections.Generic;
using Application.DTO.Request;
using Application.ViewModels;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Returns task by id.
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns>Task data</returns>
        public TaskDto GetTask(int id);

        /// <summary>
        /// Returns all tasks for employee.
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>List of tasks</returns>
        public List<TaskDto> GetTasksForEmployee(int employeeId);

        /// <summary>
        /// Creates task for employee.
        /// </summary>
        /// <param name="task">Task data to create</param>
        /// <returns>Created task</returns>
        public TaskDto InsertTask(TaskCreateRequestDto task);

        /// <summary>
        /// Deletes task by id.
        /// </summary>
        /// <param name="id">Task id</param>
        public void DeleteTask(int id);
    }
}
