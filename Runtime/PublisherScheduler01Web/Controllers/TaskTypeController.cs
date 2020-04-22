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
using PublisherScheduler01Web.ViewModels;
using System.Security.Claims;
using PublisherScheduler01Web.Repositories;

namespace PublisherScheduler01Web.Controllers
{
    [Authorize]
    public class TaskTypeController : Controller
    {
        private ISchedulerRepository _repository;

        public TaskTypeController(ISchedulerRepository repository)
        {
            _repository = repository;
        }        
        
        // GET: TaskType
        public ActionResult Index()
        {
            return View(_repository.GetTaskTypes());
        }

        // GET: TaskType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskType taskType = _repository.GetTaskTypeById(id);
            if (taskType == null)
            {
                return HttpNotFound();
            }
            TaskTypeViewModel taskTypeViewModel = new TaskTypeViewModel()
            {
                TaskTypeDetail = taskType,
                RolesSelected = GetRolesSelected(taskType),
                RoleNamesSelected = GetRoleNamesSelected(taskType),
                Roles = GetRoles()
            };

            return View(taskTypeViewModel);
        }

        // GET: TaskType/Create
        public ActionResult Create()
        {
            TaskTypeViewModel taskTypeViewModel = new TaskTypeViewModel()
            {
                RolesSelected = new string[] { },
                RoleNamesSelected = new string[] { },
                Roles = GetRoles()
            };

            return View(taskTypeViewModel);
        }

        // POST: TaskType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskTypeViewModel taskTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                taskTypeViewModel.TaskTypeDetail.Roles = new List<Capacity>();
                foreach (var r in taskTypeViewModel.RolesSelected)
                {
                    Capacity newRole = _repository.GetRoleById(Convert.ToInt16(r));
                    taskTypeViewModel.TaskTypeDetail.Roles.Add(newRole);
                }
                _repository.CreateTaskType(taskTypeViewModel.TaskTypeDetail);
                return RedirectToAction("Index");
            }

            return View(taskTypeViewModel);
        }

        // GET: TaskType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskType taskType = _repository.GetTaskTypeById(id);
            if (taskType == null)
            {
                return HttpNotFound();
            }
            TaskTypeViewModel taskTypeViewModel = new TaskTypeViewModel()
            {
                TaskTypeDetail = taskType,
                RolesSelected = GetRolesSelected(taskType),
                RoleNamesSelected = GetRoleNamesSelected(taskType),
                Roles = GetRoles()
            };
            return View(taskTypeViewModel);
        }

        // POST: TaskType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskTypeViewModel taskTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                taskTypeViewModel.TaskTypeDetail.Roles = new List<Capacity>();
                foreach (var r in taskTypeViewModel.RolesSelected)
                {
                    Capacity newRole = _repository.GetRoleById(Convert.ToInt16(r));
                    taskTypeViewModel.TaskTypeDetail.Roles.Add(newRole);
                }
                _repository.TaskTypeSaveChanges(taskTypeViewModel.TaskTypeDetail);
                return RedirectToAction("Index");
            }
            return View(taskTypeViewModel);
        }

        // GET: TaskType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskType taskType = _repository.GetTaskTypeById(id);
            if (taskType == null)
            {
                return HttpNotFound();
            }
            return View(taskType);
        }

        // POST: TaskType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteTaskType(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private string[] GetRolesSelected(TaskType taskType)
        {
            List<string> roles = new List<string>();

            if (taskType.Roles != null)
            {
                foreach (var role in taskType.Roles)
                {
                    int? id = role.Id;
                    roles.Add(id == null ? "" : role.Id.ToString());
                }
            }

            return roles.ToArray();
        }

        ICollection<SelectListItem> GetRoles()
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

        private string[] GetRoleNamesSelected(TaskType taskType)
        {
            List<string> roles = new List<string>();

            if (taskType.Roles != null)
            {
                foreach (var role in taskType.Roles)
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
