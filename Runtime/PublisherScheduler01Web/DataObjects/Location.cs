using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherScheduler01Web.DataObjects
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter location description")]
        [Display(Description = "Location description")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Slot> Slots { get; set; }
    }
}
