using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PublisherScheduler01Web.DataObjects;
using PublisherScheduler01Web.Models;

namespace PublisherScheduler01Web.Repositories
{
    public class SchedulerEdmRepository : ISchedulerRepository
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public void AssignmentSaveChanges(Assignment assignment)
        {
            db.Entry(assignment).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void CreateAssignment(Assignment assignment)
        {
            db.Assignments.Add(assignment);
            db.SaveChanges();
        }

        public void CreateLocation(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
        }

        public void CreatePerson(Person person)
        {
            db.Persons.Add(person);
            db.SaveChanges();
        }

        public void CreatePersonAvailability(PersonAvail personAvail)
        {
            db.PersonAvails.Add(personAvail);
            db.SaveChanges();

        }

        public void CreateRole(Capacity capacity)
        {
            db.Capacities.Add(capacity);
            db.SaveChanges();

        }

        public void CreateSlot(Slot slot)
        {
            db.Slots.Add(slot);
            db.SaveChanges();
        }

        public void CreateTaskType(TaskType taskType)
        {
            db.TaskTypes.Add(taskType);
            db.SaveChanges();
        }

        public void DeleteAssignment(int? id)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment != null)
            {
                db.Assignments.Remove(assignment);
                db.SaveChanges();
            }
        }

        public void DeleteLocation(int? id)
        {
            Location location = db.Locations.Find(id);
            if (location != null)
            {
                db.Locations.Remove(location);
                db.SaveChanges();
            }
        }

        public void DeletePerson(int? id)
        {
            Person person = db.Persons.Find(id);
            if (person != null)
            {
                db.Persons.Remove(person);
                db.SaveChanges();
            }
        }

        public void DeletePersonAvailability(int? id)
        {
            PersonAvail personAvail = db.PersonAvails.Find(id);
            if (personAvail != null)
            {
                db.PersonAvails.Remove(personAvail);
                db.SaveChanges();
            }
        }

        public void DeleteRole(int? id)
        {
            Capacity capacity = db.Capacities.Find(id);
            if (capacity != null)
            {
                db.Capacities.Remove(capacity);
                db.SaveChanges();
            }

        }

        public void DeleteSlot(int? id)
        {
            Slot slot = db.Slots.Find(id);
            if (slot != null)
            {
                db.Slots.Remove(slot);
                db.SaveChanges();
            }
        }

        public void DeleteTaskType(int? id)
        {
            TaskType taskType = db.TaskTypes.Find(id);
            if (taskType != null)
            {
                db.TaskTypes.Remove(taskType);
                db.SaveChanges();
            }
        }

        public Assignment GetAssignmentById(int? id)
        {
            Assignment assignment = db.Assignments.Find(id);
            return assignment;
        }

        public IEnumerable<Assignment> GetAssignments()
        {
            return db.Assignments.ToList();
        }

        public IEnumerable<Location> GetLocations()
        {
            return db.Locations.ToList();
        }

        public Location GetLocationById(int? id)
        {
            Location location = db.Locations.Find(id);
            return location;
        }

        public IEnumerable<PersonAvail> GetPersonAvailabilities()
        {
            return db.PersonAvails.ToList();
        }

        public PersonAvail GetPersonAvailabilityById(int? id)
        {
            PersonAvail personAvail = db.PersonAvails.Find(id);
            return personAvail;
        }

        public Person GetPersonById(int? id)
        {
            Person person = db.Persons.Find(id);
            return person;
        }

        public IEnumerable<Person> GetPersons()
        {
            return db.Persons.ToList();
        }

        public IEnumerable<Capacity> GetRoles()
        {
            return db.Capacities.ToList();
        }

        public Capacity GetRoleById(int? id)
        {
            Capacity capacity = db.Capacities.Find(id);
            return capacity;
        }

        public Slot GetSlotById(int? id)
        {
            Slot slot = db.Slots.Find(id);
            return slot;
        }

        public IEnumerable<Slot> GetSlots()
        {
            return db.Slots.ToList();
        }

        public TaskType GetTaskTypeById(int? id)
        {
            TaskType taskType = db.TaskTypes.Find(id);
            return taskType;
        }

        public IEnumerable<TaskType> GetTaskTypes()
        {
            return db.TaskTypes.ToList();
        }

        public void LocationSaveChanges(Location location)
        {
            throw new NotImplementedException();
        }

        public void PersonAvailabilitySaveChanges(PersonAvail personAvail)
        {
            throw new NotImplementedException();
        }

        public void PersonSaveChanges(Person person)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Location> PopulateLocationDropDownList()
        {
            return db.Locations.OrderBy(l => l.Name);
        }

        public IQueryable<Capacity> PopulateRolesDropDownList()
        {
            return db.Capacities.OrderBy(c => c.Name);
        }

        public IQueryable<TaskType> PopulateTaskTypeDropDownList()
        {
            return db.TaskTypes.OrderBy(t => t.Name);
        }

        public void RoleSaveChanges(Capacity capacity)
        {
            throw new NotImplementedException();
        }

        public void SlotSaveChanges(Slot slot)
        {
            throw new NotImplementedException();
        }

        public void TaskTypeSaveChanges(TaskType taskType)
        {
            throw new NotImplementedException();
        }
    }
}