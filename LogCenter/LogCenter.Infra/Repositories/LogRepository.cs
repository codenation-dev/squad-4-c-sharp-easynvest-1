using System;
using System.Linq;
using System.Threading.Tasks;
using LogCenter.Domain.DTOs;
using LogCenter.Domain.Entities;
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

        public async Task<int> Count(int id)
        {
            var log = await GetById(id);
            if (log == null)
                return 0;

            return Database.Logs
                .Where(x => x.Title == log.Title && x.Environment == log.Environment)
                .Count();
        }

        public async override Task<Log> GetById(int id)
        {
            return await GetLogs().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PaginatedResult<LogDTO>> GetLogs(LogQuery urlQuery)
        {
            var query = GetLogs();

            query = query.Where(x => x.Archived == urlQuery.Archived);

            if (!string.IsNullOrWhiteSpace(urlQuery.Title))
                query = query.Where(x => x.Title == urlQuery.Title);

            if (urlQuery.Level.HasValue)
                query = query.Where(x => x.Level == urlQuery.Level);

            if (!string.IsNullOrWhiteSpace(urlQuery.Origin))
                query = query.Where(x => x.Origin == urlQuery.Origin);

            if (urlQuery.UserId > 0)
                query = query.Where(x => x.User.Id == urlQuery.UserId);

            if (urlQuery.Environment != null)
                query = query.Where(x => x.Environment == urlQuery.Environment.Value);

            query.OrderByDescending(x => x.CreationDate);

            var totalItems = await query.LongCountAsync();

            query = query.Skip(urlQuery.PageSize * urlQuery.PageIndex)
                         .Take(urlQuery.PageSize);

            var logs = await query.ToListAsync();

            return new PaginatedResult<LogDTO>(logs.Select(x => new LogDTO(x)), urlQuery.PageIndex, urlQuery.PageSize, totalItems);
        }
    }
}
