namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.ViewModels;
    using Domain.Enums;
    using Domain.Models;
    using Infrastructure.Repositories;

    public class TimeIntervalService
    {
        private TimeIntervalRepository _timeIntervalRepository;

        public TimeIntervalService(TimeIntervalRepository timeIntervalRepository)
        {
            _timeIntervalRepository = timeIntervalRepository;
        }

        public List<TimeIntervalDto> GetTimeIntervalsForEmployee(int employeeId)
        {
            return _timeIntervalRepository.GetTimeIntervalsForEmployee(employeeId).
                Select(x => new TimeIntervalDto(x)).ToList();
        }

        public List<float> GetTimeStatsForPeriod(int employeeId, int projectId, string rawDateStart, string rawDateEnd)
        {
            DateTime dateStart = DateTime.Parse(rawDateStart);
            DateTime dateEnd = DateTime.Parse(rawDateEnd);
            var dayIntervals = new List<float>();
            var intervals = _timeIntervalRepository.GetTimeIntervalsForEmployeeAndProject(employeeId, projectId);
            for (DateTime date = dateStart; date <= dateEnd; date = date.AddDays(1))
            {
                float sum = 0;
                intervals.Where(i => i.Date == date).ToList().ForEach(i => sum += i.Duration);
                dayIntervals.Add(sum);
            }

            return dayIntervals;
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
    }
}
