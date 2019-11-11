using LogCenter.Domain.DTOs;
using LogCenter.Domain.Enums;
using LogCenter.Domain.Results;
using System.Net;

namespace LogCenter.Domain.Entities
{
    public class Log : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public LevelType Level { get; set; }
        public string Origin { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public Log()
        {
        }

        public Log(LogDTO logDTO)
        {
            Title = logDTO.Title;
            Description = logDTO.Description;
            Level = logDTO.Level;
            Origin = logDTO.Origin;
            UserId = logDTO.User.Id;
        }

        public override Result IsValid()
        {
            var result = new Result { StatusCode = HttpStatusCode.OK };

            if (string.IsNullOrWhiteSpace(Title))
                result.AddError("Invalid log", "Invalid log title", GetType().FullName);

            if (string.IsNullOrWhiteSpace(Description))
                result.AddError("Invalid log", "Invalid log description", GetType().FullName);

            if (string.IsNullOrWhiteSpace(Origin))
                result.AddError("Invalid log", "Invalid log origin", GetType().FullName);

            if (UserId <= 0)
                result.AddError("Invalid log", "Invalid log user", GetType().FullName);

            return result;
        }
    }
}
