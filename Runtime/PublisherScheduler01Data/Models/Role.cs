using System.Collections.Generic;

namespace PublisherScheduler01Data.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}