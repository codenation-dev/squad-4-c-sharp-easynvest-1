using LogCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogCenter.Domain.UrlQuery
{
    public class LogQuery : UrlQueryBase
    {
        public string Title { get; set; }
        public LevelType? Level { get; set; }
        public Ambiente? Ambiente { get; set; }
        public string Origin { get; set; }
        public int UserId { get; set; }
    }
}
