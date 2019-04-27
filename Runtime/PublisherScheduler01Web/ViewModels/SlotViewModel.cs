using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublisherScheduler01Web.DataObjects;

namespace PublisherScheduler01Web.ViewModels
{
    public class SlotViewModel
    {
        public Slot SlotDetail { get; set; }

        [Display(Name = "Location ")]
        public string LocationName { get; set; }

        public ICollection<SelectListItem> LocationsAvailable { get; set; }


    }
}