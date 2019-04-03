using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherScheduler01Web.DataObjects
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Capacity> Capacities { get; set; }
        public virtual ICollection<SlotFill> SlotFills { get; set; }
    }
}
