using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublisherScheduler01Web.DataObjects
{
    public class Slot
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Begin { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        [Required(ErrorMessage = "Please select a location")]
        public int? LocationId { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
