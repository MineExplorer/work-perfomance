using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class TimeIntervalService: ITimeIntervalService
    {
        private ITimeIntervalRepository _timeIntervalRepository;

        public TimeIntervalService(ITimeIntervalRepository timeIntervalRepository)
        {
            _timeIntervalRepository = timeIntervalRepository;
        }

        public async Task<TimeIntervalDto> GetAsync(int id)
        {
            TimeInterval result = await _timeIntervalRepository.GetAsync(id);
            return new TimeIntervalDto(result);
        }

        public async Task<List<TimeIntervalDto>> GetAllForEmployeeAsync(int employeeId, string rawDateStart, string rawDateEnd)
        {
            DateTime dateStart = DateTime.Parse(rawDateStart);
            DateTime dateEnd = DateTime.Parse(rawDateEnd);
            var list = await _timeIntervalRepository.GetAllForEmployeeAsync(employeeId, dateStart, dateEnd);
            return list.Select(x => new TimeIntervalDto(x)).ToList();
        }

        public async Task<TimeIntervalDto> CreateAsync(TimeIntervalCreateRequestDto timeInterval)
        {
            TimeInterval result = await _timeIntervalRepository.CreateAsync(timeInterval.ToModel());
            return new TimeIntervalDto(result);
        }

        public async Task<TimeIntervalDto> UpdateAsync(int id, TimeIntervalCreateRequestDto timeInterval)
        {
            TimeInterval result = await _timeIntervalRepository.UpdateAsync(id, timeInterval.ToModel());
            return new TimeIntervalDto(result);
        }

        public async Task DeleteAsync(int id)
        {
            await _timeIntervalRepository.DeleteAsync(id);
        }

        public async Task<Dictionary<int, float>> GetTotalTimeByWorkTypeAsync(int employeeId, string rawDateStart, string rawDateEnd)
        {
            DateTime dateStart = DateTime.Parse(rawDateStart);
            DateTime dateEnd = DateTime.Parse(rawDateEnd);
            var timeByWorkType = new Dictionary<int, float>();
            var list = await _timeIntervalRepository.GetAllForEmployeeAsync(employeeId, dateStart, dateEnd);
            list.ForEach(i => {
                timeByWorkType.TryGetValue(i.WorkType.Id, out float value);
                timeByWorkType[i.WorkType.Id] = value + i.Duration;
            });
            return timeByWorkType;
        }

        public Dictionary<int, List<float>> GetTimeStatsForPeriod(int employeeId, string rawDateStart, string rawDateEnd)
        {
            DateTime dateStart = DateTime.Parse(rawDateStart);
            DateTime dateEnd = DateTime.Parse(rawDateEnd);
            var workingTimeByProject = new Dictionary<int, List<float>>();
            var intervalsByProject = _timeIntervalRepository.GetForEmployeeByProjects(employeeId, dateStart, dateEnd);
            foreach (var pair in intervalsByProject)
            {
                if (pair.Value.Count == 0) continue;

                workingTimeByProject.Add(pair.Key, CalculateSumOfIntervalsByDay(pair.Value, dateStart, dateEnd));
            }
            return workingTimeByProject;
        }

        public Dictionary<int, List<float>> GetTeamStatsForPeriod(int projectId, string rawDateStart, string rawDateEnd)
        {
            DateTime dateStart = DateTime.Parse(rawDateStart);
            DateTime dateEnd = DateTime.Parse(rawDateEnd);
            var workingTimeByEmployee = new Dictionary<int, List<float>>();
            var intervalsByEmployee = _timeIntervalRepository.GetForProjectByEmployees(projectId, dateStart, dateEnd);
            foreach (var pair in intervalsByEmployee)
            {
                workingTimeByEmployee.Add(pair.Key, CalculateSumOfIntervalsByDay(pair.Value, dateStart, dateEnd));
            }

            return workingTimeByEmployee;
        }

        private List<float> CalculateSumOfIntervalsByDay(List<TimeInterval> intervals, DateTime dateStart, DateTime dateEnd)
        {
            var workingHoursPerDay = new List<float>();
            for (DateTime date = dateStart; date <= dateEnd; date = date.AddDays(1))
            {
                float sum = 0;
                intervals.Where(i => i.Date == date).ToList().ForEach(i => sum += i.Duration);
                workingHoursPerDay.Add(sum);
            }
            return workingHoursPerDay;
        }
    }
}
