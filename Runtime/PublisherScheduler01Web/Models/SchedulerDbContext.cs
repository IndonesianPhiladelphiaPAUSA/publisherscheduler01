using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PublisherScheduler01Web.DataObjects;
using PublisherSchedulerIdentity;
using System.Security.Claims;

namespace PublisherScheduler01Web.Models
{
    public class SchedulerDbContext : ApplicationDbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Capacity> Capacities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<PersonAvail> PersonAvails { get; set; }


    }
}