using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO.Request;
using Application.ViewModels;

namespace Application.Interfaces
{
    public interface ITimeIntervalService
    {
        /// <summary>
        /// Returns time interval by id
        /// </summary>
        /// <param name="id">Time interval id</param>
        /// <returns>Time interval data</returns>
        Task<TimeIntervalDto> GetAsync(int id);

        /// <summary>
        /// Returns all time intervals for employee
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <param name="rawDateStart">Date start string</param>
        /// <param name="rawDateEnd">Date end string</param>
        /// <returns>List of time intervals</returns>
        Task<List<TimeIntervalDto>> GetAllForEmployeeAsync(int employeeId, string rawDateStart, string rawDateEnd);

        /// <summary>
        /// Creates time interval
        /// </summary>
        /// <param name="timeInterval">Time interval data to create</param>
        /// <returns>Created time interval</returns>
        Task<TimeIntervalDto> CreateAsync(TimeIntervalCreateRequestDto timeInterval);

        /// <summary>
        /// Updates time interval
        /// </summary>
        /// <param name="id">Time interval id</param>
        /// <param name="timeInterval">Time interval data to update</param>
        /// <returns>Updated time interval</returns>
        Task<TimeIntervalDto> UpdateAsync(int id, TimeIntervalCreateRequestDto timeInterval);

        /// <summary>
        /// Deletes time interval by id
        /// </summary>
        /// <param name="id">Time interval id</param>
        Task DeleteAsync(int id);

        Task<Dictionary<int, float>> GetTotalTimeByWorkTypeAsync(int employeeId, string rawDateStart, string rawDateEnd);

        Dictionary<int, List<float>> GetTimeStatsForPeriod(int employeeId, string rawDateStart, string rawDateEnd);

        Dictionary<int, List<float>> GetTeamStatsForPeriod(int projectId, string rawDateStart, string rawDateEnd);
    }
}
