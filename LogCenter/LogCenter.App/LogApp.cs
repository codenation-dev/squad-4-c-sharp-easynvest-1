using LogCenter.Domain.DTOs;
using LogCenter.Domain.Entities;
using LogCenter.Domain.Results;
using LogCenter.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogCenter.App
{
    public class LogApp
    {
        private readonly LogRepository _logRepository;

        public LogApp(LogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<Result<LogDTO>> SaveLog(LogDTO logDTO)
        {
            var result = new Result<LogDTO> { StatusCode = HttpStatusCode.OK };

            var log = new Log(logDTO);

            _logRepository.Save(log);

            await _logRepository.CommitAsync();

            log = await _logRepository.GetById(log.Id);

            result.Value = new LogDTO(log);

            return result;
        }
    }
}
