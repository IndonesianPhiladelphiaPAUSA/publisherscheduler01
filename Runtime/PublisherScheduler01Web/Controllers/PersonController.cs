using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PublisherScheduler01Web.Repositories;
using PublisherScheduler01Web.DataObjects;
using PublisherScheduler01Web.Models;
using PublisherScheduler01Web.ViewModels;

namespace PublisherScheduler01Web.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private ISchedulerRepository _repository;

        public PersonController(ISchedulerRepository repository)
        {
            _repository = repository;
        }
        
        // GET: Person
        public ActionResult Index()
        {
            return View(_repository.GetPersons());
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = _repository.GetPersonById(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            PersonViewModel personViewModel = new PersonViewModel(_repository) { PersonDetail = person };

            return View(personViewModel);

        }

        // GET: Person/Create
        public ActionResult Create()
        {
            Person person = new Person();
            PersonViewModel personViewModel = new PersonViewModel(_repository)
            {
                PersonDetail = person,
                RolesSelected = { }
            };

            return View(personViewModel);
        }
            
        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsActive")] Person person)
        {
            if (ModelState.IsValid)
            {
                _repository.CreatePerson(person);
                return RedirectToAction("Index");
            }

            PersonViewModel personViewModel = new PersonViewModel(_repository)
            {
                PersonDetail = person,
                RolesSelected = { }
            };

            return View(personViewModel);
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = _repository.GetPersonById(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            PersonViewModel personEditViewModel = new PersonViewModel(_repository) { PersonDetail = person };

            return View(personEditViewModel);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsActive")] Person person)
        {
            if (ModelState.IsValid)
            {
                _repository.PersonSaveChanges(person);
                return RedirectToAction("Index");
            }
            PersonViewModel personEditViewModel = new PersonViewModel(_repository) { PersonDetail = person };

            return View(personEditViewModel);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = _repository.GetPersonById(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeletePerson(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void PopulateRolesDropDownList(int? selectedRole = null)
        {
            var roles = _repository.PopulateRolesDropDownList();
            ViewBag.RoleId = new SelectList(roles.AsNoTracking(), "RoleId", "RoleName", selectedRole);
        }
    }
}
