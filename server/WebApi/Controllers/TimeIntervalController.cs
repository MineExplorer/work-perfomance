using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Exeptions;

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
            catch (ObjectNotFoundException ex)
            {
                return TimeIntervalNotFound(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex.InnerException);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TimeIntervalDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetForEmployee(int employeeId, string dateStart, string dateEnd)
        {
            try
            {
                return Ok(await _timeIntervalService.GetAllForEmployeeAsync(employeeId, dateStart, dateEnd));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
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
            catch (ObjectNotFoundException ex)
            {
                return TimeIntervalNotFound(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
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


        [HttpGet("worktypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dictionary<int, float>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetTotalTimeByWorkType(int employeeId, string dateStart, string dateEnd)
        {
            try
            {
                return Ok(await _timeIntervalService.GetTotalTimeByWorkTypeAsync(employeeId, dateStart, dateEnd));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpGet("stats")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dictionary<int, List<float>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult GetTimeStatsForPeriod(int employeeId, string dateStart, string dateEnd)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dictionary<int, List<float>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult GetTeamStatsForPeriod(int projectId, string dateStart, string dateEnd)
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

        private ActionResult TimeIntervalNotFound(ObjectNotFoundException ex)
        {
            var error = new ErrorDto
            {
                Code = "NotFound",
                Message = ex.Message,
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
