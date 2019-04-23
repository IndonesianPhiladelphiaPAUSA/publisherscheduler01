using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PublisherScheduler01Web.DataObjects;

namespace PublisherScheduler01Web.ViewModels
{
    public class SlotViewModel
    {
        public Slot SlotDetail { get; set; }

        [Display(Description = "Location Name: ")]
        public string LocationName { get; set; }
    }
}