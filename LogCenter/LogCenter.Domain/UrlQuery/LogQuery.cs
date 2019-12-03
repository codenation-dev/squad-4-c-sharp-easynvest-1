﻿using LogCenter.Domain.Entities;
using LogCenter.Domain.Enums;

namespace LogCenter.Domain.UrlQuery
{
    public class LogQuery : UrlQueryBase
    {
        public string Title { get; set; }
        public LevelType? Level { get; set; }

        public Environment? Environment { get; set; }
        public string Origin { get; set; }
        public int UserId { get; set; }
        public bool Archived { get; set; }
    }
}
