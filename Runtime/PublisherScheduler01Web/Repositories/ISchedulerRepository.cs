using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using PublisherScheduler01Web.DataObjects;

namespace PublisherScheduler01Web.Repositories
{
    public interface ISchedulerRepository
    {
        // Assignment CRUD
        IEnumerable<Assignment> GetAssignments();
        Assignment GetAssignmentById(int? id);
        void CreateAssignment(Assignment assignment);
        void DeleteAssignment(int? id);
        void AssignmentSaveChanges(Assignment assignment);
        
        // Person CRUD
        IEnumerable<Person> GetPersons();
        Person GetPersonById(int? id);
        void CreatePerson(Person person);
        void DeletePerson(int? id);
        void PersonSaveChanges(Person person);

        // Capacity CRUD
        IEnumerable<Capacity> GetRoles();
        Capacity GetRoleById(int? id);
        void CreateRole(Capacity capacity);
        void DeleteRole(int? id);
        void RoleSaveChanges(Capacity capacity);

        // Location CRUD
        IEnumerable<Location> GetLocations();
        Location GetLocationById(int? id);
        void CreateLocation(Location location);
        void DeleteLocation(int? id);
        void LocationSaveChanges(Location location);

        // Person Availability CRUD
        IEnumerable<PersonAvail> GetPersonAvailabilities();
        PersonAvail GetPersonAvailabilityById(int? id);
        void CreatePersonAvailability(PersonAvail personAvail);
        void DeletePersonAvailability(int? id);
        void PersonAvailabilitySaveChanges(PersonAvail personAvail);

        // Slot CRUD
        IEnumerable<Slot> GetSlots();
        Slot GetSlotById(int? id);
        void CreateSlot(Slot slot);
        void DeleteSlot(int? id);
        void SlotSaveChanges(Slot slot);

        // TaskType CRUD
        IEnumerable<TaskType> GetTaskTypes();
        TaskType GetTaskTypeById(int? id);
        void CreateTaskType(TaskType taskType);
        void DeleteTaskType(int? id);
        void TaskTypeSaveChanges(TaskType taskType);

        // AspNet 
        IEnumerable<IdentityRole> GetIdentityRoles();

        IQueryable<Location> PopulateLocationsDropDownList();
        IQueryable<TaskType> PopulateTaskTypesDropDownList();
        IQueryable<Capacity> PopulateRolesDropDownList();
        IQueryable<Slot> PopulateSlotsDropDownList();
        IQueryable<Person> PopulatePersonsDropDownList();

    }
}
