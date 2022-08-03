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
        TaskDto GetTask(int id);

        /// <summary>
        /// Returns all tasks for employee.
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>List of tasks</returns>
        List<TaskDto> GetTasksForEmployee(int employeeId);

        /// <summary>
        /// Creates task for employee.
        /// </summary>
        /// <param name="task">Task data to create</param>
        /// <returns>Created task</returns>
        TaskDto InsertTask(TaskCreateRequestDto task);

        /// <summary>
        /// Deletes task by id.
        /// </summary>
        /// <param name="id">Task id</param>
        void DeleteTask(int id);
    }
}
