using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PublisherScheduler01Web.Models;
using PublisherSchedulerIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublisherScheduler01Web.Controllers
{
    [Authorize]
    public class AspNetRoleController : Controller
    {
        SchedulerDbContext context = new SchedulerDbContext();
        
        // GET: Role
        public ActionResult Index()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            var roles = context.Roles.ToList();

            return View(roles);
        }


        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {


                if (!IsAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = userManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Administrator")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}