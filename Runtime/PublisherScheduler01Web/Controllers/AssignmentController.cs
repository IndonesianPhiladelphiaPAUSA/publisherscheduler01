using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PublisherScheduler01Web.DataObjects;
using PublisherScheduler01Web.Models;
using PublisherScheduler01Web.Repositories;
using PublisherScheduler01Web.ViewModels;

namespace PublisherScheduler01Web.Controllers
{
    [Authorize]
    public class AssignmentController : Controller
    {
        ISchedulerRepository _repository;

        public AssignmentController(ISchedulerRepository repository)
        {
            _repository = repository;
        }

        // GET: Assignment
        public ActionResult Index()
        {
            IList<AssignmentViewModel> assignmentsVm = new List<AssignmentViewModel>();

            foreach (Assignment a in _repository.GetAssignments())
            {
                assignmentsVm.Add(new AssignmentViewModel()
                {
                    AssignmentDetail = a,
                    SlotSelected = _repository.GetSlotById(a.SlotId).Name,
                    TaskTypeSelected = _repository.GetTaskTypeById(a.TaskTypeId).Name,
                    PersonSelected = _repository.GetPersonById(a.PersonId).Name,
                });
            }

            return View(assignmentsVm);
        }

        // GET: Assignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = _repository.GetAssignmentById(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }

            Slot slot = _repository.GetSlotById(assignment.SlotId);
            TaskType taskType = _repository.GetTaskTypeById(assignment.TaskTypeId);
            Person person = _repository.GetPersonById(assignment.PersonId);
            AssignmentViewModel assignmentViewModel = new AssignmentViewModel()
            {
                AssignmentDetail = assignment,
                SlotSelected = slot == null ? "" : slot.Name,
                TaskTypeSelected = taskType == null ? "" : taskType.Name,
                PersonSelected = person == null ? "" : person.Name,
            };

            return View(assignmentViewModel);
        }

        // GET: Assignment/Create
        public ActionResult Create()
        {
            AssignmentViewModel assignmentViewModel = new AssignmentViewModel()
            {
                SlotsAvailable = GetAvailableSlots(),
                TaskTypesAvailable = GetAvailableTaskTypes(),
                PersonsAvailable = GetAvailablePersons()
            };

            return View(assignmentViewModel);
        }

        // POST: Assignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssignmentViewModel assignmentViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateAssignment(assignmentViewModel.AssignmentDetail);
                return RedirectToAction("Index");
            }

            return View(assignmentViewModel);
        }

        // GET: Assignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = _repository.GetAssignmentById(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }

            Slot slot = _repository.GetSlotById(assignment.SlotId);
            TaskType taskType = _repository.GetTaskTypeById(assignment.TaskTypeId);
            Person person = _repository.GetPersonById(assignment.PersonId);
            AssignmentViewModel assignmentViewModel = new AssignmentViewModel()
            {
                AssignmentDetail = assignment,
                SlotSelected = slot == null ? "" : slot.Name,
                TaskTypeSelected = taskType == null ? "" : taskType.Name,
                PersonSelected = person == null ? "" : person.Name,
                SlotIdString = assignment.SlotId.ToString(),
                TaskTypeIdString = assignment.TaskTypeId.ToString(),
                PersonIdString = assignment.PersonId.ToString(),
                SlotsAvailable = GetAvailableSlots(),
                TaskTypesAvailable = GetAvailableTaskTypes(),
                PersonsAvailable = GetAvailablePersons()
            };

            return View(assignmentViewModel);
        }

        // POST: Assignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssignmentViewModel assignmentViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.AssignmentSaveChanges(assignmentViewModel.AssignmentDetail);
                return RedirectToAction("Index");
            }

            return View(assignmentViewModel);
        }

        // GET: Assignment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = _repository.GetAssignmentById(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            AssignmentViewModel assignmentViewModel = new AssignmentViewModel()
            {
                AssignmentDetail = assignment,
                SlotSelected = _repository.GetSlotById(assignment.SlotId).Name,
                TaskTypeSelected = _repository.GetTaskTypeById(assignment.TaskTypeId).Name,
                PersonSelected = _repository.GetPersonById(assignment.PersonId).Name
            };

            return View(assignmentViewModel);
        }

        // POST: Assignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteAssignment(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private ICollection<SelectListItem> GetAvailableSlots()
        {
            var allSlots = _repository.GetSlots().ToList();

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (allSlots != null)
            {
                foreach (var slot in allSlots)
                {
                    selectListItems.Add(new SelectListItem { Text = slot.Name, Value = slot.Id.ToString() });
                }
            }

            return selectListItems;
        }

        private ICollection<SelectListItem> GetAvailableTaskTypes()
        {
            var allTaskTypes = _repository.GetTaskTypes().ToList();

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (allTaskTypes != null)
            {
                foreach (var taskType in allTaskTypes)
                {
                    selectListItems.Add(new SelectListItem { Text = taskType.Name, Value = taskType.Id.ToString() });
                }
            }

            return selectListItems;
        }

        private ICollection<SelectListItem> GetAvailablePersons()
        {
            var allPersons = _repository.GetPersons().ToList();

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (allPersons != null)
            {
                foreach (var person in allPersons)
                {
                    selectListItems.Add(new SelectListItem { Text = person.Name, Value = person.Id.ToString() });
                }
            }

            return selectListItems;
        }

        private void PopulateSlotsDropDownList(int? selectedSlot = null)
        {
            var slots = _repository.PopulateSlotsDropDownList();
            ViewBag.RoleId = new SelectList(slots.AsNoTracking(), "SlotId", "SlotName", selectedSlot);
        }

        private void PopulateTaskTypesDropDownList(int? selectedTaskType = null)
        {
            var taskTypes = _repository.PopulateTaskTypesDropDownList();
            ViewBag.RoleId = new SelectList(taskTypes.AsNoTracking(), "TaskTypeId", "TaskTypeName", selectedTaskType);
        }

        private void PopulatePersonsDropDownList(int? selectedPerson = null)
        {
            var persons = _repository.PopulateTaskTypesDropDownList();
            ViewBag.RoleId = new SelectList(persons.AsNoTracking(), "PersonId", "PersonName", selectedPerson);
        }
    }
}
