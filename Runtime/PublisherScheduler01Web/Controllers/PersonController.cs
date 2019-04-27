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
using System.Security.Claims;

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

            PersonViewModel personViewModel = new PersonViewModel()
            {
                PersonDetail = person,
                RolesSelected = GetRolesSelected(person),
                RoleNamesSelected = GetRoleNamesSelected(person),
                Capacities = GetCapacities(person)
            };

            return View(personViewModel);

        }

        // GET: Person/Create
        public ActionResult Create()
        {
            try
            {
                var publisherName = ((ClaimsIdentity)User.Identity).FindFirst("PublisherName");

                // Find out if this publisher has a person record already
                Person person = _repository.GetPersons().FirstOrDefault(p => p.Name == publisherName.Value);

                if (person == null)
                {
                    // no record found, create a new person with that name
                    person = new Person()
                    {
                        Name = publisherName.Value,
                        IsActive = true,
                        SecurityLevel = (int)Constants.SecurityLevel.User
                    };
                }
                else
                {
                    // Cannot create other person if not authorized
                    if (person.SecurityLevel == null || person.SecurityLevel > 2)
                    {
                        ViewBag.Message = "Not enough security level to create person";
                        return View("CreateError");
                    }
                    else
                    {
                        // create a new person
                        person = new Person()
                        {
                            IsActive = true,
                            SecurityLevel = (int)Constants.SecurityLevel.User
                        };
                    }
                }

                PersonViewModel personViewModel = new PersonViewModel()
                {
                    PersonDetail = person,
                    RolesSelected = GetRolesSelected(person),
                    RoleNamesSelected = GetRoleNamesSelected(person),
                    Capacities = GetCapacities(person)
                };

                return View(personViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error : " + ex.Message;
                return View("CreateError");
            }

        }
            
        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonViewModel personViewModel)
        {
            if (ModelState.IsValid)
            {
                personViewModel.PersonDetail.Capacities = new List<Capacity>();
                foreach (var r in personViewModel.RolesSelected)
                {
                    Capacity newRole = _repository.GetRoleById(Convert.ToInt16(r));
                    personViewModel.PersonDetail.Capacities.Add(newRole);
                }
                _repository.CreatePerson(personViewModel.PersonDetail);
                return RedirectToAction("Index");
            }

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
            PersonViewModel personEditViewModel = new PersonViewModel()
            {
                PersonDetail = person,
                RolesSelected = GetRolesSelected(person),
                RoleNamesSelected = GetRoleNamesSelected(person),
                Capacities = GetCapacities(person)
            };

            return View(personEditViewModel);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonViewModel personViewModel)
        {
            if (ModelState.IsValid)
            {
                personViewModel.PersonDetail.Capacities = new List<Capacity>();
                foreach (var r in personViewModel.RolesSelected)
                {
                    Capacity newRole = _repository.GetRoleById(Convert.ToInt16(r));
                    personViewModel.PersonDetail.Capacities.Add(newRole);
                }
                _repository.PersonSaveChanges(personViewModel.PersonDetail);
                return RedirectToAction("Index");
            }

            return View(personViewModel);
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


        private string[] GetRolesSelected(Person person)
        {
            List<string> roles = new List<string>();

            if (person.Capacities != null)
            {
                foreach (var role in person.Capacities)
                {
                    int? id = role.Id;
                    roles.Add(id == null ? "" : role.Id.ToString());
                }
            }

            return roles.ToArray();
        }
        
        ICollection<SelectListItem> GetCapacities (Person person)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (_repository != null)
            {
                var allRoles = _repository.GetRoles().ToList();


                if (allRoles != null)
                {
                    foreach (var role in allRoles)
                    {
                        selectListItems.Add(new SelectListItem { Text = role.Name, Value = role.Id.ToString() });
                    }

                }
            }

            return selectListItems;
        }

        private string[] GetRoleNamesSelected(Person person)
        {
            List<string> roles = new List<string>();

            if (person.Capacities != null)
            {
                foreach (var role in person.Capacities)
                {
                    roles.Add(role.Name);
                }
            }

            return roles.ToArray();
        }

        private void PopulateRolesDropDownList(int? selectedRole = null)
        {
            var roles = _repository.PopulateRolesDropDownList();
            ViewBag.RoleId = new SelectList(roles.AsNoTracking(), "RoleId", "RoleName", selectedRole);
        }
    }
}
