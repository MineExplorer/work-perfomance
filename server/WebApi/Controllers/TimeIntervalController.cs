using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeIntervalController : ControllerBase
    {
        private readonly ILogger<TimeIntervalController> logger;
        private ITimeIntervalService _timeIntervalService;

        public TimeIntervalController(ILogger<TimeIntervalController> logger, ITimeIntervalService timeIntervalService)
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
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimeIntervalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _timeIntervalService.GetAsync(id));
            }
            catch (KeyNotFoundException)
            {
                return TimeIntervalNotFound(id);
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex.InnerException);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimeIntervalDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Create([FromBody] TimeIntervalCreateRequestDto timeInterval)
        {
            try
            {
                return Ok(await _timeIntervalService.CreateAsync(timeInterval));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimeIntervalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Update(int id, [FromBody] TimeIntervalCreateRequestDto timeInterval)
        {
            try
            {
                return Ok(await _timeIntervalService.UpdateAsync(id, timeInterval));
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _timeIntervalService.DeleteAsync(id);
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
