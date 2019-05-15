using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PublisherScheduler01Web.DataObjects;
using PublisherScheduler01Web.Models;

namespace PublisherScheduler01Web.Repositories
{
    public class SchedulerEdmRepository : ISchedulerRepository
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        #region GetAll Section
        public IEnumerable<Assignment> GetAssignments()
        {
            return db.Assignments.ToList();
        }

        public IEnumerable<Capacity> GetRoles()
        {
            return db.Capacities.ToList();
        }

        public IEnumerable<Location> GetLocations()
        {
            return db.Locations.ToList();
        }

        public IEnumerable<Person> GetPersons()
        {
            return db.Persons.ToList();
        }

        public IEnumerable<PersonAvail> GetPersonAvailabilities()
        {
            return db.PersonAvails.ToList();
        }

        public IEnumerable<Slot> GetSlots()
        {
            return db.Slots.ToList();
        }

        public IEnumerable<TaskType> GetTaskTypes()
        {
            return db.TaskTypes.ToList();
        }

        public IEnumerable<IdentityRole> GetIdentityRoles()
        {
            return db.Roles.ToList();
        }

        #endregion

        #region GetOne Section
        public Assignment GetAssignmentById(int? id)
        {
            Assignment assignment = db.Assignments.Find(id);
            return assignment;
        }

        public Capacity GetRoleById(int? id)
        {
            Capacity capacity = db.Capacities.Find(id);
            return capacity;
        }

        public Person GetPersonById(int? id)
        {
            Person person = db.Persons.Find(id);
            return person;
        }

        public PersonAvail GetPersonAvailabilityById(int? id)
        {
            PersonAvail personAvail = db.PersonAvails.Find(id);
            return personAvail;
        }

        public Location GetLocationById(int? id)
        {
            Location location = db.Locations.Find(id);
            return location;
        }

        public Slot GetSlotById(int? id)
        {
            Slot slot = db.Slots.Find(id);
            return slot;
        }


        public TaskType GetTaskTypeById(int? id)
        {
            TaskType taskType = db.TaskTypes.Find(id);
            return taskType;
        }

        #endregion

        #region Create Section
        public void CreateAssignment(Assignment assignment)
        {
            db.Assignments.Add(assignment);
            db.SaveChanges();
        }

        public void CreateRole(Capacity capacity)
        {
            db.Capacities.Add(capacity);
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

        #endregion Create Section

        #region Save Section
        public void AssignmentSaveChanges(Assignment assignment)
        {
            db.Entry(assignment).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RoleSaveChanges(Capacity capacity)
        {
            db.Entry(capacity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void LocationSaveChanges(Location location)
        {
            db.Entry(location).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void PersonSaveChanges(Person person)
        {
            // using method in https://stackoverflow.com/questions/27176014/how-to-add-update-child-entities-when-updating-a-parent-entity-in-ef

            var existingPerson = db.Persons
                .Where(p => p.Id == person.Id)
                .Include(p => p.Roles)
                .SingleOrDefault();

            if (existingPerson != null)
            {
                // update parent
                db.Entry(existingPerson).CurrentValues.SetValues(person);

                // delete children
                foreach (var existingRole in existingPerson.Roles.ToList())
                {
                    if (!person.Roles.Any(r => r.Id == existingRole.Id))
                        existingPerson.Roles.Remove(existingRole);
                }

                // Update and Insert children
                foreach (var role in person.Roles)
                {
                    var existingRole = existingPerson.Roles
                        .Where(r => r.Id == role.Id)
                        .SingleOrDefault();

                    if (existingRole != null)
                        // Update child
                        db.Entry(existingRole).CurrentValues.SetValues(role);
                    else
                    {
                        // Insert child
                        var newRole = GetRoleById(role.Id);
                        existingPerson.Roles.Add(newRole);
                    }
                }
            }
            // db.Entry(person).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void PersonAvailabilitySaveChanges(PersonAvail personAvail)
        {
            db.Entry(personAvail).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SlotSaveChanges(Slot slot)
        {
            db.Entry(slot).State = EntityState.Modified;
            db.SaveChanges();

        }

        public void TaskTypeSaveChanges(TaskType taskType)
        {
            db.Entry(taskType).State = EntityState.Modified;
            db.SaveChanges();
        }

        #endregion Save Section

        #region Delete Section
        public void DeleteAssignment(int? id)
        {
            Assignment assignment = db.Assignments.Find(id);
            if (assignment != null)
            {
                db.Assignments.Remove(assignment);
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
                // delete assignments
                foreach (var existingAssignment in db.Assignments.Where(a => a.PersonId == id).ToList())
                {
                    db.Assignments.Remove(existingAssignment);
                }

                // delete person availability
                foreach (var existingAvailability in db.PersonAvails.Where(a => a.PersonId == id).ToList())
                {
                    db.PersonAvails.Remove(existingAvailability);
                }

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

        #endregion Delete Section

        #region Populate List Section
        public IQueryable<Location> PopulateLocationsDropDownList()
        {
            return db.Locations.OrderBy(l => l.Name);
        }

        public IQueryable<Capacity> PopulateRolesDropDownList()
        {
            return db.Capacities.OrderBy(c => c.Name);
        }

        public IQueryable<Slot> PopulateSlotsDropDownList()
        {
            return db.Slots.OrderBy(c => c.Name);
        }

        public IQueryable<Person> PopulatePersonsDropDownList()
        {
            return db.Persons.OrderBy(c => c.Name);
        }

        public IQueryable<TaskType> PopulateTaskTypesDropDownList()
        {
            return db.TaskTypes.OrderBy(t => t.Name);
        }

        #endregion

        #region User Claim Section
        public string GetRole()
        {
            //var publisherName = ((ClaimsIdentity)User.Identity).FindFirst("PublisherName");

            //var user = User.Identity;
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var s = userManager.GetRoles(user.GetUserId());

            return "";
        }
        #endregion


    }
}