using System;
using System.Collections.Generic;

namespace PublisherScheduler01Data.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public int? TaskId { get; set; }
        public int? LocationId { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}