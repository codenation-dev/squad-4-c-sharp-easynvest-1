using System;
using System.Collections.Generic;
using System.Text;

namespace LogCenter.Domain.UrlQuery
{
    public abstract class UrlQueryBase
    {
        public int PageSize { get; set; } = 50;
        public int PageIndex { get; set; } = 0;
        public string OrderBy { get; set; }
    }
}
