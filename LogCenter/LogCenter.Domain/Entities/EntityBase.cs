using System;
using System.Collections.Generic;
using System.Text;

namespace LogCenter.Domain.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
