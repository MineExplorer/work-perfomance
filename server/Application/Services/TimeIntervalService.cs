using System;
using System.Collections.Generic;
using System.Linq;
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

        
        public TimeIntervalDto GetTimeInterval(int id)
        {
            TimeInterval result = _timeIntervalRepository.Get(id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new TimeIntervalDto(result);
        }

        public List<TimeIntervalDto> GetTimeIntervalsForEmployee(int employeeId, string rawDateStart, string rawDateEnd)
        {
            DateTime dateStart = DateTime.Parse(rawDateStart);
            DateTime dateEnd = DateTime.Parse(rawDateEnd);
            return _timeIntervalRepository.GetAllForEmployee(employeeId, dateStart, dateEnd).
                Select(x => new TimeIntervalDto(x)).ToList();
        }

        public TimeIntervalDto InsertTimeInterval(TimeIntervalCreateRequestDto timeInterval)
        {
            return new TimeIntervalDto(_timeIntervalRepository.Create(timeInterval.ToModel()));
        }

        public TimeIntervalDto UpdateTimeInterval(int id, TimeIntervalCreateRequestDto timeInterval)
        {
            TimeInterval result = _timeIntervalRepository.Update(id, timeInterval.ToModel());
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new TimeIntervalDto(result);
        }

        public void DeleteTimeInterval(int id)
        {
            _timeIntervalRepository.Delete(id);
        }

        public Dictionary<int, float> GetTotalTimeByWorkType(int employeeId, string rawDateStart, string rawDateEnd)
        {
            DateTime dateStart = DateTime.Parse(rawDateStart);
            DateTime dateEnd = DateTime.Parse(rawDateEnd);
            var timeByWorkType = new Dictionary<int, float>();
            _timeIntervalRepository.GetAllForEmployee(employeeId, dateStart, dateEnd).
                ForEach(i =>
                {
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
