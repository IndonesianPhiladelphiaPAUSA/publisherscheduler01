using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublisherScheduler01Web.DataObjects
{
    public class SlotFill
    {
        public int Id { get; set; }
        public int SlotId { get; set; }
        public int AssignmentId { get; set; }
        public int PersonId { get; set; }
    }
}