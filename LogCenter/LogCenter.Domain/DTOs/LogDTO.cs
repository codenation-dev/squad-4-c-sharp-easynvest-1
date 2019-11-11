using LogCenter.Domain.Entities;
using LogCenter.Domain.Enums;

namespace LogCenter.Domain.DTOs
{
    public class LogDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public LevelType Level { get; set; }
        public string Origin { get; set; }
        public UserDTO User { get; set; }

        public LogDTO()
        {

        }

        public LogDTO(Log log)
        {
            Id = log.Id;
            Title = log.Title;
            Description = log.Description;
            Level = log.Level;
            Origin = log.Origin;
            User = new UserDTO(log.User);
        }
    }
}
