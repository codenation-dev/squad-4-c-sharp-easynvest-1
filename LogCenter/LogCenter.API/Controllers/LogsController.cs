using System;
using System.Net;
using System.Threading.Tasks;
using LogCenter.App;
using LogCenter.Domain.DTOs;
using LogCenter.Domain.Results;
using LogCenter.Domain.UrlQuery;
using LogCenter.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LogCenter.API.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly LogRepository _logRepository;
        private readonly LogApp _logApp;
        public LogsController(LogRepository logRepository, LogApp logApp)
        {
            _logRepository = logRepository;
            _logApp = logApp;
        }

        [HttpGet()]
        public async Task<IActionResult> GetLogs([FromQuery] LogQuery urlQuery)
        {
            var result = new PaginatedResult<LogDTO>() { StatusCode = HttpStatusCode.OK };

            try
            {
                result = await _logRepository.GetLogs(urlQuery);

                if (result.IsFailure)
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.AddError("Error on GetLogs", ex.Message, GetType().FullName);

                return StatusCode((int)HttpStatusCode.InternalServerError, result);
            }

            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> SaveLog([FromBody] LogDTO log)
        {
            var result = new Result<LogDTO>() { StatusCode = HttpStatusCode.OK };

            try
            {
                result = await _logApp.SaveLog(log);

                if (result.IsFailure)
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.AddError("Error on SaveLog", ex.Message, GetType().FullName);

                return StatusCode((int)HttpStatusCode.InternalServerError, result);
            }

            return Ok(result);
        }

        [HttpPost("[action]/{logId}")]
        public async Task<IActionResult> Archive(int logId)
        {
            try
            {
                var log = await _logRepository.GetById(logId);

                if (log == null)
                    return StatusCode((int)HttpStatusCode.NotFound, $"Log with id {logId} was not found");

                if (log.Archived)
                    return StatusCode((int)HttpStatusCode.Conflict, $"Log with id {logId} is already archived");

                log.Archived = true;
                _logRepository.Update(log);
                await _logRepository.CommitAsync();
                
                return Ok("Log successfully archived");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

    }
}
