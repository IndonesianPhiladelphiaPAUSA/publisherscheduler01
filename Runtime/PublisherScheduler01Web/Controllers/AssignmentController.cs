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
                assignmentsVm.Add(new AssignmentViewModel(_repository)
                {
                    AssignmentDetail = a,
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

            AssignmentViewModel assignmentViewModel = new AssignmentViewModel(_repository) { AssignmentDetail = assignment };

            return View(assignmentViewModel);
        }

        // GET: Assignment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SlotId,TaskTypeId,PersonId")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateAssignment(assignment);
                return RedirectToAction("Index");
            }

            AssignmentViewModel assignmentViewModel = new AssignmentViewModel(_repository) { AssignmentDetail = assignment };

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
            AssignmentViewModel assignmentViewModel = new AssignmentViewModel(_repository) { AssignmentDetail = assignment };

            return View(assignmentViewModel);
        }

        // POST: Assignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SlotId,TaskTypeId,PersonId")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _repository.AssignmentSaveChanges(assignment);
                return RedirectToAction("Index");
            }
            AssignmentViewModel assignmentViewModel = new AssignmentViewModel(_repository) { AssignmentDetail = assignment };

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
            AssignmentViewModel assignmentViewModel = new AssignmentViewModel(_repository) { AssignmentDetail = assignment };

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
