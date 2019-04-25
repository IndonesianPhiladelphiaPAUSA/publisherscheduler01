using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherScheduler01Web.DataObjects
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [Display(Description = "Full name: ")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? SecurityLevel { get; set; }

        [Display(Description = "Role: ")]
        public virtual ICollection<Capacity> Capacities { get; set; }
        public virtual ICollection<PersonAvail> PersonAvails { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
