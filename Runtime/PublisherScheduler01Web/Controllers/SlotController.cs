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
                var location = _repository.GetLocationById(s.LocationId);
                slotsVm.Add(new ViewModels.SlotViewModel()
                {
                    SlotDetail = s,
                    LocationName = location == null ? "" : location.Name 
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
            slotVm.LocationName = _repository.GetLocationById(slot.LocationId).Name;
            return View(slotVm);
        }

        // GET: Slot/Create
        public ActionResult Create()
        {
            SlotViewModel slotViewModel = new SlotViewModel()
            {
                LocationsAvailable = GetLocations()
            };

            return View(slotViewModel);
        }

        // POST: Slot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SlotViewModel slotViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateSlot(slotViewModel.SlotDetail);
                return RedirectToAction("Index");
            }

            return View(slotViewModel);
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

            SlotViewModel slotViewModel = new SlotViewModel()
            {
                SlotDetail = slot,
                LocationIdString = slot.LocationId.ToString(),
                LocationsAvailable = GetLocations()
            };

            return View(slotViewModel);
        }

        // POST: Slot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SlotViewModel slotViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.SlotSaveChanges(slotViewModel.SlotDetail);
                return RedirectToAction("Index");
            }
            return View(slotViewModel);
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

            SlotViewModel slotViewModel = new SlotViewModel()
            {
                SlotDetail = slot,
                LocationName = _repository.GetLocationById(slot.LocationId).Name
            };
            return View(slotViewModel);
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

        ICollection<SelectListItem> GetLocations()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (_repository != null)
            {
                var allLocations = _repository.GetLocations().ToList();


                if (allLocations != null)
                {
                    foreach (var location in allLocations)
                    {
                        selectListItems.Add(new SelectListItem { Text = location.Name, Value = location.Id.ToString() });
                    }

                }
            }

            return selectListItems;
        }

    }
}
