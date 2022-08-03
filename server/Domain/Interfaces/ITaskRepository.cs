using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        IQueryable<TaskEntity> GetAll();

        /// <summary>
        /// Returns task by id.
        /// </summary>
        /// <param name="id">TaskEntity id</param>
        /// <returns>TaskEntity data</returns>
        TaskEntity Get(int id);

        /// <summary>
        /// Returns all tasks for employee.
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>List of tasks</returns>
        List<TaskEntity> GetAllForEmployee(int employeeId);

        /// <summary>
        /// Creates task for employee.
        /// </summary>
        /// <param name="task">TaskEntity data to create</param>
        /// <returns>Created task</returns>
        TaskEntity Create(TaskEntity task);

        /// <summary>
        /// Deletes task by id.
        /// </summary>
        /// <param name="id">TaskEntity id</param>
        void Delete(int id);
    }
}
