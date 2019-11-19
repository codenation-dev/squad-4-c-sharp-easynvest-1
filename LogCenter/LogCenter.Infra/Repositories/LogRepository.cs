using System;
using System.Linq;
using System.Threading.Tasks;
using LogCenter.Domain.DTOs;
using LogCenter.Domain.Entities;
using LogCenter.Domain.Enums;
using LogCenter.Domain.Results;
using LogCenter.Domain.UrlQuery;
using LogCenter.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace LogCenter.Infra.Repositories
{
    public class LogRepository : BaseRepository<Log>
    {
        public LogRepository(DatabaseContext context)
            : base(context)
        {
        }

        private IQueryable<Log> GetLogs()
        {
            return Database.Logs.Include(x => x.User);
        }

        public async override Task<Log> GetById(int id)
        {
            return await GetLogs().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PaginatedResult<LogDTO>> GetLogs(LogQuery urlQuery)
        {
            var query = GetLogs();

            if (!string.IsNullOrWhiteSpace(urlQuery.Title))
                query = query.Where(x => x.Title == urlQuery.Title);

            if (urlQuery.Level.HasValue)
                query = query.Where(x => x.Level == urlQuery.Level);

            if (urlQuery.Ambiente.HasValue)
                query = query.Where(x => x.Ambiente == urlQuery.Ambiente);

            if (!string.IsNullOrWhiteSpace(urlQuery.Origin))
                query = query.Where(x => x.Origin == urlQuery.Origin);

            if (urlQuery.UserId > 0)
                query = query.Where(x => x.User.Id == urlQuery.UserId);

            query.OrderByDescending(x => x.CreationDate);

            var totalItems = await query.LongCountAsync();

            query = query.Skip(urlQuery.PageSize * urlQuery.PageIndex)
                         .Take(urlQuery.PageSize);

            var logs = await query.ToListAsync();

            return new PaginatedResult<LogDTO>(logs.Select(x => new LogDTO(x)), urlQuery.PageIndex, urlQuery.PageSize, totalItems);
        }


        // orderby  - 0 | warn - 1 | error - 2
        public async Task<PaginatedResult<LogDTO>> GetLevels(LogQuery urlQuery)
        {
            var query = GetLogs();

            if (!string.IsNullOrWhiteSpace(urlQuery.Title))
                query = query.Where(x => x.Title == urlQuery.Title);

            if (urlQuery.Level.HasValue)
                query = query.Where(x => x.Level == urlQuery.Level);

            if (urlQuery.Ambiente.HasValue)
                query = query.Where(x => x.Ambiente == urlQuery.Ambiente);

            if (!string.IsNullOrWhiteSpace(urlQuery.Origin))
                query = query.Where(x => x.Origin == urlQuery.Origin);

            if (urlQuery.UserId > 0)
                query = query.Where(x => x.User.Id == urlQuery.UserId);

            query.OrderBy(x => x.Level);

            var totalItems = await query.LongCountAsync();

            query = query.Skip(urlQuery.PageSize * urlQuery.PageIndex)
                         .Take(urlQuery.PageSize);

            var logs = await query.ToListAsync();

            return new PaginatedResult<LogDTO>(logs.Select(x => new LogDTO(x)), urlQuery.PageIndex, urlQuery.PageSize, totalItems);
        }

    }
}