namespace PublisherScheduler01Web.Migrations
{
    using PublisherScheduler01Web.DataObjects;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PublisherScheduler01Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PublisherScheduler01Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            Debugger.Launch();

            var capacities = new List<Capacity>
            {
                new Capacity{Name="Elder"},
                new Capacity{Name="MS"},
                new Capacity{Name="Brother"},
                new Capacity{Name="Sister"},
                new Capacity{Name="Pioneer"},
                new Capacity{Name="Publisher"}
            };
            capacities.ForEach(c => context.Capacities.Add(c));
            context.SaveChanges();

            var assignments = new List<Assignment>
            {
                new Assignment
                {
                    Name = "Publisher",
                    IsActive = true,
                    Capacities = (List<Capacity>)capacities.Where(c => (new List<string>() { "Elder", "MS", "Brother", "Sister", "Pioneer", "Publisher" }).Contains(c.Name)).ToList()
                },
                new Assignment
                {
                    Name = "Captain",
                    IsActive = true,
                    Capacities = (List<Capacity>)capacities.Where(c => (new List<string>() { "Elder", "MS", "Brother" }).Contains(c.Name)).ToList()
                }
            };
            assignments.ForEach(a => context.Assignments.Add(a));
            context.SaveChanges();

            var locations = new List<Location>
            {
                new Location{Name = "Wing Phat", IsActive = true},
                new Location{Name = "One Stop Market", IsActive = true}
            };
            locations.ForEach(l => context.Locations.Add(l));
            context.SaveChanges();

            var slots = new List<Slot>
            {
                new Slot{Begin = DateTime.Parse("2019-04-03 14:00:00"),
                    End = DateTime.Parse("2019-04-03 16:00:00"),
                    IsActive = true,
                    LocationId = locations.Where(l => l.Name == "One Stop Market").ToList()[0].Id},
                new Slot{Begin = DateTime.Parse("2019-04-03 16:00:00"),
                    End = DateTime.Parse("2019-04-03 18:00:00"),
                    IsActive = true,
                    LocationId = locations.Where(l => l.Name == "One Stop Market").ToList()[0].Id },
                new Slot{Begin = DateTime.Parse("2019-04-06 12:30:00"),
                    End = DateTime.Parse("2019-04-06 14:00:00"),
                    IsActive = true,
                    LocationId = locations.Where(l => l.Name == "One Stop Market").ToList()[0].Id },
                new Slot{Begin = DateTime.Parse("2019-04-06 14:00:00"),
                    End = DateTime.Parse("2019-04-06 15:30:00"),
                    IsActive = true,
                    LocationId = locations.Where(l => l.Name == "One Stop Market").ToList()[0].Id },
                new Slot{Begin = DateTime.Parse("2019-04-06 12:30:00"),
                    End = DateTime.Parse("2019-04-06 14:00:00"),
                    IsActive = true,
                    LocationId = locations.Where(l => l.Name == "Wing Phat").ToList()[0].Id },
                new Slot{Begin = DateTime.Parse("2019-04-06 14:00:00"),
                    End = DateTime.Parse("2019-04-06 15:30:00"),
                    IsActive = true,
                    LocationId = locations.Where(l => l.Name == "Wing Phat").ToList()[0].Id },
                new Slot{Begin = DateTime.Parse("2019-04-07 11:00:00"),
                    End = DateTime.Parse("2019-04-07 12:30:00"),
                    IsActive = true,
                    LocationId = locations.Where(l => l.Name == "One Stop Market").ToList()[0].Id },
                new Slot{Begin = DateTime.Parse("2019-04-07 12:30:00"),
                    End = DateTime.Parse("2019-04-07 14:00:00"),
                    IsActive = true,
                    LocationId = locations.Where(l => l.Name == "One Stop Market").ToList()[0].Id },
            };
            slots.ForEach(s => context.Slots.Add(s));
            context.SaveChanges();

            var persons = new List<Person>
            {
                new Person{Name = "Peter Subianto",
                    IsActive = true,
                    Capacities = (List<Capacity>)capacities.Where(c => (new List<string>() {"Elder","Brother","Pioneer","Publisher" }).Contains(c.Name)).ToList()},
                new Person{Name = "Keith Floyd",
                    IsActive = true,
                    Capacities = (List<Capacity>)capacities.Where(c => (new List<string>() {"MS","Brother","Publisher" }).Contains(c.Name)).ToList()},
                new Person{Name = "Dawn Lancaster",
                    IsActive = true,
                    Capacities = (List<Capacity>)capacities.Where(c => (new List<string>() {"Sister","Pioneer","Publisher" }).Contains(c.Name)).ToList()}
            };
            persons.ForEach(p => context.Persons.Add(p));
            context.SaveChanges();

            var personAvails = new List<PersonAvail>
            {
                new PersonAvail{UserId = persons.Where(p => p.Name == "Peter Subianto").ToList()[0].Id,
                    Begin = DateTime.Parse("2019-04-03 14:00:00"),
                    End = DateTime.Parse("2019-04-03 16:00:00"),
                    IsAvailable = true},
                new PersonAvail{UserId = persons.Where(p => p.Name == "Keith Floyd").ToList()[0].Id,
                    Begin = DateTime.Parse("2019-04-03 14:00:00"),
                    End = DateTime.Parse("2019-04-03 16:00:00"),
                    IsAvailable = true},
                new PersonAvail{UserId = persons.Where(p => p.Name == "Dawn Lancaster").ToList()[0].Id,
                    Begin = DateTime.Parse("2019-04-03 14:00:00"),
                    End = DateTime.Parse("2019-04-03 16:00:00"),
                    IsAvailable = false}
            };
            personAvails.ForEach(p => context.PersonAvails.Add(p));
            context.SaveChanges();

            var slotFills = new List<SlotFill>
            {
                new SlotFill{
                    SlotId = slots.Where(s => 
                            (s.LocationId == locations.Where(l => l.Name == "One Stop Market").ToList()[0].Id && 
                             s.Begin.DayOfWeek.Equals(DayOfWeek.Wednesday))
                            ).ToList()[0].Id,
                    AssignmentId = assignments.Where(a => a.Name == "Captain").ToList()[0].Id,
                    PersonId = persons.Where(p => p.Name == "Peter Subianto").ToList()[0].Id},
                new SlotFill{
                    SlotId = slots.Where(s =>
                            (s.LocationId == locations.Where(l => l.Name == "One Stop Market").ToList()[0].Id &&
                             s.Begin.DayOfWeek.Equals(DayOfWeek.Wednesday))
                            ).ToList()[0].Id,
                    AssignmentId = assignments.Where(a => a.Name == "Publisher").ToList()[0].Id,
                    PersonId = persons.Where(p => p.Name == "Keith Floyd").ToList()[0].Id}
            };
            slotFills.ForEach(s => context.SlotFills.Add(s));
            context.SaveChanges();
        }
    }
}
