using LogCenter.Domain.Results;
using System;

namespace LogCenter.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        public abstract Result IsValid();
    }
}
