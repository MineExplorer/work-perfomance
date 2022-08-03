using System.Collections.Generic;
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
        TimeIntervalDto GetTimeInterval(int id);

        /// <summary>
        /// Returns all time intervals for employee
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <param name="rawDateStart">Date start string</param>
        /// <param name="rawDateEnd">Date end string</param>
        /// <returns>List of time intervals</returns>
        List<TimeIntervalDto> GetTimeIntervalsForEmployee(int employeeId, string rawDateStart, string rawDateEnd);

        /// <summary>
        /// Creates time interval
        /// </summary>
        /// <param name="timeInterval">Time interval data to create</param>
        /// <returns>Created time interval</returns>
        TimeIntervalDto InsertTimeInterval(TimeIntervalCreateRequestDto timeInterval);

        /// <summary>
        /// Updates time interval
        /// </summary>
        /// <param name="id">Time interval id</param>
        /// <param name="timeInterval">Time interval data to update</param>
        /// <returns>Updated time interval</returns>
        TimeIntervalDto UpdateTimeInterval(int id, TimeIntervalCreateRequestDto timeInterval);

        /// <summary>
        /// Deletes time interval by id
        /// </summary>
        /// <param name="id">Time interval id</param>
        void DeleteTimeInterval(int id);

        Dictionary<int, float> GetTotalTimeByWorkType(int employeeId, string rawDateStart, string rawDateEnd);

        Dictionary<int, List<float>> GetTimeStatsForPeriod(int employeeId, string rawDateStart, string rawDateEnd);

        Dictionary<int, List<float>> GetTeamStatsForPeriod(int projectId, string rawDateStart, string rawDateEnd);
    }
}
