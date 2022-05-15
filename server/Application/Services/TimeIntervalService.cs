namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.ViewModels;
    using Domain.Models;
    using Infrastructure.Repositories;

    public class TimeIntervalService
    {
        private TimeIntervalRepository _timeIntervalRepository;

        public TimeIntervalService(TimeIntervalRepository timeIntervalRepository)
        {
            _timeIntervalRepository = timeIntervalRepository;
        }

        public List<TimeIntervalDto> GetTimeIntervalsForEmployee(int employeeId, string rawDateStart, string rawDateEnd)
        {
            DateTime dateStart = DateTime.Parse(rawDateStart);
            DateTime dateEnd = DateTime.Parse(rawDateEnd);
            return _timeIntervalRepository.GetTimeIntervalsForEmployee(employeeId, dateStart, dateEnd).
                Select(x => new TimeIntervalDto(x)).ToList();
        }

        public Dictionary<int, List<float>> GetTimeStatsForPeriod(int employeeId, string rawDateStart, string rawDateEnd)
        {
            DateTime dateStart = DateTime.Parse(rawDateStart);
            DateTime dateEnd = DateTime.Parse(rawDateEnd);
            var workingTimeByProject = new Dictionary<int, List<float>>();
            var intervalsByProject = _timeIntervalRepository.GetTimeIntervalsForEmployeeProjects(employeeId, dateStart, dateEnd);
            foreach(var pair in intervalsByProject)
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
            var intervalsByEmployee = _timeIntervalRepository.GetTimeIntervalsForProjectEmployees(projectId, dateStart, dateEnd);
            foreach (var pair in intervalsByEmployee)
            {
                workingTimeByEmployee.Add(pair.Key, CalculateSumOfIntervalsByDay(pair.Value, dateStart, dateEnd));
            }

            return workingTimeByEmployee;
        }

        public TimeIntervalDto InsertTimeInterval(TimeIntervalCreateRequestDto timeInterval)
        {
            return new TimeIntervalDto(_timeIntervalRepository.InsertTimeInterval(timeInterval.ToModel()));
        }
        /*
        public TimeIntervalDto GetTimeInterval(int id)
        {
            TimeInterval result = _timeIntervalRepository.GetTimeInterval(id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new TimeIntervalDto(result);
        }*/

        public TimeIntervalDto UpdateTimeInterval(int id, TimeIntervalCreateRequestDto timeInterval)
        {
            TimeInterval result = _timeIntervalRepository.UpdateTimeInterval(id, timeInterval.ToModel());
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new TimeIntervalDto(result);
        }

        public void DeleteTimeInterval(int id)
        {
            _timeIntervalRepository.DeleteTimeInterval(id);
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
