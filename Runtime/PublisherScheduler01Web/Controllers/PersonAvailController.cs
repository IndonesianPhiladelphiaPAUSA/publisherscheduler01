using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
                    PersonName = _repository.GetPersonById(pa.PersonId) == null ? "" : _repository.GetPersonById(pa.PersonId).Name
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

            PersonAvailViewModel personAvailVm = new PersonAvailViewModel()
            {
                PersonAvailDetail = personAvail,
                PersonName = _repository.GetPersonById(personAvail.PersonId) == null ? "" : _repository.GetPersonById(personAvail.PersonId).Name
            };
            
            return View(personAvailVm);
        }

        // GET: PersonAvail/Create
        public ActionResult Create()
        {
            var publisherName = ((ClaimsIdentity)User.Identity).FindFirst("PublisherName");

            // Find out if this publisher has a person record already
            Person person = _repository.GetPersons().FirstOrDefault(p => p.Name == publisherName.Value);

            PersonAvailViewModel personAvailViewModel = new PersonAvailViewModel();

            //var user = User.Identity;
            //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //    var s = userManager.GetRoles(user.GetUserId());
            //    if (s[0].ToString() == "Administrator")
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }

            if (person.SecurityLevel == Convert.ToInt16(PublisherScheduler01Web.DataObjects.Constants.SecurityLevel.User))
            {
                personAvailViewModel.PersonList = new List<SelectListItem>() { new SelectListItem() { Text = person.Name, Value = person.Id.ToString() } };
            }
            else
            {
                personAvailViewModel.PersonList = GetPersonsList();
            }

            return View(personAvailViewModel);
        }

        // POST: PersonAvail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonAvailViewModel personAvailViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.CreatePersonAvailability(personAvailViewModel.PersonAvailDetail);
                return RedirectToAction("Index");
            }

            return View(personAvailViewModel);
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

        ICollection<SelectListItem> GetPersonsList()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (_repository != null)
            {
                var allPersons = _repository.GetPersons().ToList();

                if (allPersons != null)
                {
                    foreach (var role in allPersons)
                    {
                        selectListItems.Add(new SelectListItem { Text = role.Name, Value = role.Id.ToString() });
                    }

                }
            }

            return selectListItems;
        }

    }
}
