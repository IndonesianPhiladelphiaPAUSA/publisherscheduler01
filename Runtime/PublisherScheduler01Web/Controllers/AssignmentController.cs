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
            IEnumerable<Assignment> allItems = _repository.GetAssignments();

            return View(allItems);
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
            return View(assignment);
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
                return RedirectToAction("Index");
            }

            return View(assignment);
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
            return View(assignment);
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
            return View(assignment);
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
            return View(assignment);
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
    }
}
