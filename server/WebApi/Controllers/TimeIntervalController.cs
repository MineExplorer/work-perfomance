using System;
using System.Collections.Generic;
using Application.DTO.Request;
using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeIntervalController : ControllerBase
    {
        private readonly ILogger<TimeIntervalController> logger;
        private TimeIntervalService _timeIntervalService;

        public TimeIntervalController(ILogger<TimeIntervalController> logger, TimeIntervalService timeIntervalService)
        {
            this.logger = logger;
            _timeIntervalService = timeIntervalService;
        }

        [HttpGet]
        public ActionResult<List<TimeIntervalDto>> GetForEmployee(int employeeId, string dateStart, string dateEnd)
        {
            try
            {
                return Ok(_timeIntervalService.GetTimeIntervalsForEmployee(employeeId, dateStart, dateEnd));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpGet("worktypes")]
        public ActionResult<Dictionary<int, float>> GetTotalTimeByWorkType(int employeeId, string dateStart, string dateEnd)
        {
            try
            {
                return Ok(_timeIntervalService.GetTotalTimeByWorkType(employeeId, dateStart, dateEnd));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpGet("stats")]
        public ActionResult<Dictionary<int, List<float>>> GetTimeStatsForPeriod(int employeeId, string dateStart, string dateEnd)
        {
            try
            {
                return Ok(_timeIntervalService.GetTimeStatsForPeriod(employeeId, dateStart, dateEnd));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }
        
        [HttpGet("teamstats")]
        public ActionResult<Dictionary<int, List<float>>> GetTeamStatsForPeriod(int projectId, string dateStart, string dateEnd)
        {
            try
            {
                return Ok(_timeIntervalService.GetTeamStatsForPeriod(projectId, dateStart, dateEnd));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }
        /*
        [HttpGet("{id}")]
        public ActionResult<TimeIntervalDto> Get(int id)
        {
            try
            {
                return Ok(_timeIntervalService.GetTimeInterval(id));
            }
            catch (KeyNotFoundException)
            {
                return TimeIntervalNotFound(id);
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex.InnerException);
            }
        }*/

        [HttpPost]
        public ActionResult<TimeIntervalDto> Insert([FromBody] TimeIntervalCreateRequestDto timeInterval)
        {
            try
            {
                return Ok(_timeIntervalService.InsertTimeInterval(timeInterval));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<TimeIntervalDto> Update(int id, [FromBody] TimeIntervalCreateRequestDto timeInterval)
        {
            try
            {
                return Ok(_timeIntervalService.UpdateTimeInterval(id, timeInterval));
            }
            catch (KeyNotFoundException)
            {
                return TimeIntervalNotFound(id);
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _timeIntervalService.DeleteTimeInterval(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        private ActionResult TimeIntervalNotFound(int id)
        {
            var error = new ErrorDto
            {
                Code = "NotFound",
                Message = $"TimeInterval with id {id} not found",
                Target = "TimeIntervalId",
            };

            return NotFound(error);
        }

        private ActionResult InternalErrorResult(Exception ex)
        {
            var error = new ErrorDto
            {
                Code = "InternalError",
                Message = ex.Message,
            };

            return StatusCode(500, error);
        }
    }
}
