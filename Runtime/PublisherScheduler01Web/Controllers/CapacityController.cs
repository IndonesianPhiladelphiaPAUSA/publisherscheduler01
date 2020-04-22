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
    public class CapacityController : Controller
    {
        private ISchedulerRepository _repository;

        public CapacityController(ISchedulerRepository repository)
        {
            _repository = repository;
        }

        // GET: Capacity
        public ActionResult Index()
        {
            return View(_repository.GetRoles());
        }

        // GET: Capacity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacity capacity = _repository.GetRoleById(id);
            if (capacity == null)
            {
                return HttpNotFound();
            }
            return View(capacity);
        }

        // GET: Capacity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Capacity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateRole(new Capacity() { Name = roleViewModel.Name });
                return RedirectToAction("Index");
            }

            return View(roleViewModel);
        }

        // GET: Capacity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacity role = _repository.GetRoleById(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            RoleViewModel roleViewModel = new RoleViewModel
            {
                RoleDetail = role,
                Name = role.Name
            };

            return View(roleViewModel);
        }

        // POST: Capacity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                Capacity role = _repository.GetRoleById(roleViewModel.RoleDetail.Id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                _repository.RoleSaveChanges(roleViewModel.RoleDetail);
                return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }

        // GET: Capacity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacity role = _repository.GetRoleById(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        // POST: Capacity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteRole(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
