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
    public class SlotController : Controller
    {
        private ISchedulerRepository _repository;

        public SlotController(ISchedulerRepository repository)
        {
            _repository = repository;
        }

        // GET: Slot
        public ActionResult Index()
        {
            IList<SlotViewModel> slotsVm = new List<SlotViewModel>();

            foreach (Slot s in _repository.GetSlots().ToList())
            {
                slotsVm.Add(new ViewModels.SlotViewModel()
                {
                    SlotDetail = s,
                    LocationName = _repository.GetLocationById(s.Id) == null ? "" : _repository.GetLocationById(s.Id).Name 
                });
            }

            return View(slotsVm);
        }

        // GET: Slot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slot slot = _repository.GetSlotById(id);
            if (slot == null)
            {
                return HttpNotFound();
            }
            SlotViewModel slotVm = new SlotViewModel();
            slotVm.SlotDetail = slot;
            slotVm.LocationName = _repository.GetLocationById(slot.Id).Name;
            return View(slotVm);
        }

        // GET: Slot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Begin,End,LocationId,IsActive")] Slot slot)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateSlot(slot);
                return RedirectToAction("Index");
            }

            return View(slot);
        }

        // GET: Slot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slot slot = _repository.GetSlotById(id);
            if (slot == null)
            {
                return HttpNotFound();
            }
            return View(slot);
        }

        // POST: Slot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Begin,End,LocationId,IsActive")] Slot slot)
        {
            if (ModelState.IsValid)
            {
                _repository.SlotSaveChanges(slot);
                return RedirectToAction("Index");
            }
            return View(slot);
        }

        // GET: Slot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slot slot = _repository.GetSlotById(id);
            if (slot == null)
            {
                return HttpNotFound();
            }
            return View(slot);
        }

        // POST: Slot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteSlot(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
