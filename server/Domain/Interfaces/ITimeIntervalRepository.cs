using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface ITimeIntervalRepository
    {
        /// <summary>
        /// Returns time interval by id
        /// </summary>
        /// <param name="id">Time interval id</param>
        /// <returns>Time interval data</returns>
        TimeInterval Get(int id);

        /// <summary>
        /// Creates time interval
        /// </summary>
        /// <param name="timeInterval">Time interval data to create</param>
        /// <returns>Created time interval</returns>
        TimeInterval Create(TimeInterval timeInterval);

        // <summary>
        /// Updates time interval
        /// </summary>
        /// <param name="id">Time interval id</param>
        /// <param name="timeInterval">Time interval data to update</param>
        /// <returns>Updated time interval</returns>
        TimeInterval Update(int id, TimeInterval timeInterval);

        /// <summary>
        /// Deletes time interval by id
        /// </summary>
        /// <param name="id">Time interval id</param>
        void Delete(int id);

        List<TimeInterval> GetAllForEmployee(int employeeId, DateTime startDate, DateTime endDate);

        Dictionary<int, List<TimeInterval>> GetForEmployeeByProjects(int employeeId, DateTime startDate, DateTime endDate);

        Dictionary<int, List<TimeInterval>> GetForProjectByEmployees(int projectId, DateTime startDate, DateTime endDate);

    }
}
