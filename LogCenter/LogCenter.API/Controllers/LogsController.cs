using System;
using System.Net;
using System.Threading.Tasks;
using LogCenter.App;
using LogCenter.Domain.DTOs;
using LogCenter.Domain.Entities;
using LogCenter.Domain.Enums;
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

        [Microsoft.AspNetCore.Mvc.HttpPost()]
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

        // funcionando
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _logRepository.GetById(id);

                if (result == null)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // filtro por ambiente {homologação, produção, dev} (mesmo problema do level - precisaria de outro query}
        [HttpGet("{ambiente}")]
        public async Task<IActionResult> GetLogsAmbiente([FromQuery] LogQuery urlQuery)
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


        // ordenação por level {tá pedindo coisa demais com esse parametro - tentei outros métodos mas não rolou}
        [HttpGet("{levels}")]
        public async Task<IActionResult> GetLevels([FromQuery] LogQuery urlQuery)
        {
            var result = new PaginatedResult<LogDTO>() { StatusCode = HttpStatusCode.OK };

            try
            {
                result = await _logRepository.GetLevels(urlQuery);

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


        // ordernação por frequência (como vamos calcular?)



        // buscar por level / título / origem
        // a gente também precisa de um outro query pois esse pede muito
        [HttpGet("{search}")]
        public async Task<IActionResult> SearchLogs([FromQuery] LogQuery urlQuery)
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


        // funcionando - pede a id duas vezes (path e query)
        public async Task<IActionResult> DeleteLog([FromQuery] int id)
        {
            var result = new PaginatedResult<LogDTO>() { StatusCode = HttpStatusCode.OK };
            try
            {
                var log = await _logRepository.GetById(id);
                if (log == null)
                {
                    return BadRequest(log);
                }

                _logRepository.Delete(log);
                await _logRepository.CommitAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // arquivar está no arquivo da tati

    }
}
