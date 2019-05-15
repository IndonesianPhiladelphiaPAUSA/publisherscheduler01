using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherScheduler01Web.DataObjects
{
    public class TaskType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter task type")]
        [Display(Description = "Task type")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "Roles required")]
        public virtual ICollection<Capacity> Roles { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
