using LogCenter.Domain.Entities;
using LogCenter.Domain.Enums;
using System;

namespace LogCenter.Domain.DTOs
{
    public class LogDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public LevelType Level { get; set; }
        public Ambiente Ambiente { get; set; }
        public string Origin { get; set; }
        public UserDTO User { get; set; }
        public DateTime Date { get; set; }

        public bool Archived { get; set; }
        public LogCenter.Domain.Entities.Environment Environment { get; set; }


        public LogDTO()
        {

        }

        public LogDTO(Log log)
        {
            Id = log.Id;
            Title = log.Title;
            Description = log.Description;
            Level = log.Level;
            Ambiente = log.Ambiente;
            Origin = log.Origin;
            Archived = log.Archived;
            User = new UserDTO(log.User);
            Environment = log.Environment;
            Date = log.CreationDate;
        }
    }
}
