using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PublisherScheduler01Data.Models;

namespace PublisherScheduler01Data.DAL
{
    public class SchedulerContext : DbContext
    {
        public SchedulerContext() : base("SchedulerContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UserAvail> UserAvails { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); ;
        }
    }
}
