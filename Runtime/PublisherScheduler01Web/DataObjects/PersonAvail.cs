using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherScheduler01Web.DataObjects
{
    public class PersonAvail
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public bool IsAvailable { get; set; }
    }
}
