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

        [Display(Description = "Slot: ")]
        public int SlotId { get; set; }

        [Display(Description = "Task Type: ")]
        public int TaskTypeId { get; set; }

        [Display(Description = "Person: " )]
        public int PersonId { get; set; }
    }
}