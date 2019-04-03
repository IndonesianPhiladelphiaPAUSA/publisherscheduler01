using System.Collections.Generic;

namespace PublisherScheduler01Web.DataObjects
{
    public class Capacity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<TaskType> TaskTypes { get; set; }
    }
}