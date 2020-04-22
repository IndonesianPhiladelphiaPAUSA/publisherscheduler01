using PublisherScheduler01Data.Models;
using System;
using System.Collections.Generic;

namespace PublisherScheduler01Data.DAL
{
    public class SchedulerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchedulerContext>
    {
        protected override void Seed(SchedulerContext context)
        {
            var users = new List<User>
            {
                new User{Name = "Peter Subianto", IsActive = true},
                new User{Name = "Keith Floyd", IsActive = true},
                new User{Name = "Dawn Lancaster", IsActive = true}
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var userAvails = new List<UserAvail>
            {
                new UserAvail{UserId = 1, Begin = DateTime.Parse("2019-04-03 14:00:00"), End = DateTime.Parse("2019-04-03 16:00:00"), IsAvailable = true},
                new UserAvail{UserId = 2, Begin = DateTime.Parse("2019-04-03 14:00:00"), End = DateTime.Parse("2019-04-03 16:00:00"), IsAvailable = true},
                new UserAvail{UserId = 3, Begin = DateTime.Parse("2019-04-03 14:00:00"), End = DateTime.Parse("2019-04-03 16:00:00"), IsAvailable = false}
            };
            userAvails.ForEach(u => context.UserAvails.Add(u));
            context.SaveChanges();

            var roles = new List<Role>
            {
                new Role{Name="Elder"},
                new Role{Name="MS"},
                new Role{Name="Brother"},
                new Role{Name="Sister"},
                new Role{Name="Pioneer"},
                new Role{Name="Publisher"}
            };
            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();

            var slots = new List<Slot>
            {
                new Slot{Begin = DateTime.Parse("2019-04-03 14:00:00"), End = DateTime.Parse("2019-04-03 16:00:00"), IsActive = true },
                new Slot{Begin = DateTime.Parse("2019-04-03 16:00:00"), End = DateTime.Parse("2019-04-03 18:00:00"), IsActive = true },
                new Slot{Begin = DateTime.Parse("2019-04-06 12:30:00"), End = DateTime.Parse("2019-04-06 14:00:00"), IsActive = true },
                new Slot{Begin = DateTime.Parse("2019-04-06 14:00:00"), End = DateTime.Parse("2019-04-06 15:30:00"), IsActive = true },
                new Slot{Begin = DateTime.Parse("2019-04-07 11:00:00"), End = DateTime.Parse("2019-04-07 12:30:00"), IsActive = true },
                new Slot{Begin = DateTime.Parse("2019-04-07 12:30:00"), End = DateTime.Parse("2019-04-07 14:00:00"), IsActive = true },
            };
            slots.ForEach(s => context.Slots.Add(s));
            context.SaveChanges();

            var locations = new List<Location>
            {
                new Location{Name = "Wing Phat", IsActive = true},
                new Location{Name = "One Stop Market", IsActive = true}
            };
            locations.ForEach(l => context.Locations.Add(l));
            context.SaveChanges();

            var tasks = new List<Task>
            {
                new Task{Name = "Publisher", IsActive = true},
                new Task{Name = "Captain", IsActive = true}
            };
            tasks.ForEach(t => context.Tasks.Add(t));
            context.SaveChanges();
        }
    }
}
