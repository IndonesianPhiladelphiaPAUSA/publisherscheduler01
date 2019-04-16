using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublisherScheduler01Web.DataObjects
{
    public class Capacity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter role description")]
        [Display(Description = "Role description")]
        public string Name { get; set; }
        
        [NotMapped]
        public bool Selected { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<TaskType> TaskTypes { get; set; }
    }
}