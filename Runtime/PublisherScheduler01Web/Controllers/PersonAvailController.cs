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

namespace PublisherScheduler01Web.Controllers
{
    [Authorize]
    public class PersonAvailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonAvail
        public ActionResult Index()
        {
            return View(db.PersonAvails.ToList());
        }

        // GET: PersonAvail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonAvail personAvail = db.PersonAvails.Find(id);
            if (personAvail == null)
            {
                return HttpNotFound();
            }
            return View(personAvail);
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
                db.PersonAvails.Add(personAvail);
                db.SaveChanges();
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
            PersonAvail personAvail = db.PersonAvails.Find(id);
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
                db.Entry(personAvail).State = EntityState.Modified;
                db.SaveChanges();
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
            PersonAvail personAvail = db.PersonAvails.Find(id);
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
            PersonAvail personAvail = db.PersonAvails.Find(id);
            db.PersonAvails.Remove(personAvail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
