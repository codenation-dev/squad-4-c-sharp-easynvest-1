using LogCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogCenter.Domain.Entities
{
    public class Log: EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public LevelType Level { get; set; }
        public string Origin { get; set; }
        public User User { get; set; }
    }
}
