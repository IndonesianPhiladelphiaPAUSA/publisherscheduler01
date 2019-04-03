using System;
using System.Collections.Generic;

namespace PublisherScheduler01Web.DataObjects
{
    public class Slot
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public int? LocationId { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
