using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublisherScheduler01Web.DataObjects
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }
        public int SlotId { get; set; }
        public int TaskTypeId { get; set; }
        public int PersonId { get; set; }
    }
}