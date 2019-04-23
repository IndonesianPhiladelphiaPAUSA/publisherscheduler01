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
    public class PersonAvailController : Controller
    {
        private ISchedulerRepository _repository;

        public PersonAvailController(ISchedulerRepository repository)
        {
            _repository = repository;
        }        
        
        // GET: PersonAvail
        public ActionResult Index()
        {
            IList<PersonAvailViewModel> personAvailsVm = new List<PersonAvailViewModel>();

            foreach (PersonAvail pa in _repository.GetPersonAvailabilities().ToList())
            {
                personAvailsVm.Add(new ViewModels.PersonAvailViewModel()
                {
                    PersonAvailDetail = pa,
                    PersonName = _repository.GetPersonById(pa.UserId) == null ? "" : _repository.GetPersonById(pa.UserId).Name
                });
            }

            return View(personAvailsVm);
        }

        // GET: PersonAvail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonAvail personAvail = _repository.GetPersonAvailabilityById(id);
            if (personAvail == null)
            {
                return HttpNotFound();
            }

            PersonAvailViewModel personAvailVm = new PersonAvailViewModel();
            personAvailVm.PersonAvailDetail = personAvail;
            personAvailVm.PersonName = _repository.GetPersonById(personAvail.UserId) == null ? "" : _repository.GetPersonById(personAvail.UserId).Name;

            return View(personAvailVm);
        }

        // GET: PersonAvail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonAvail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Begin,End,IsAvailable")] PersonAvail personAvail)
        {
            if (ModelState.IsValid)
            {
                _repository.CreatePersonAvailability(personAvail);
                return RedirectToAction("Index");
            }

            return View(personAvail);
        }

        // GET: PersonAvail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonAvail personAvail = _repository.GetPersonAvailabilityById(id);
            if (personAvail == null)
            {
                return HttpNotFound();
            }
            return View(personAvail);
        }

        // POST: PersonAvail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Begin,End,IsAvailable")] PersonAvail personAvail)
        {
            if (ModelState.IsValid)
            {
                _repository.PersonAvailabilitySaveChanges(personAvail);
                return RedirectToAction("Index");
            }
            return View(personAvail);
        }

        // GET: PersonAvail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonAvail personAvail =  _repository.GetPersonAvailabilityById(id);
            if (personAvail == null)
            {
                return HttpNotFound();
            }
            return View(personAvail);
        }

        // POST: PersonAvail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeletePersonAvailability(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
