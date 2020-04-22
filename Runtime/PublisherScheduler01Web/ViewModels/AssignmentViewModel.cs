using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublisherScheduler01Web.DataObjects;
using PublisherScheduler01Web.Repositories;

namespace PublisherScheduler01Web.ViewModels
{
    public class AssignmentViewModel
    {
        public Assignment AssignmentDetail { get; set; }

        [Display(Name = "Slot")]
        public string SlotSelected { get; set; }

        [Display(Name = "Task Type")]
        public string TaskTypeSelected { get; set; }

        [Display(Name = "Person")]
        public string PersonSelected { get; set; }

        public string SlotIdString { get; set; }

        public string TaskTypeIdString { get; set; }

        public string PersonIdString { get; set; }

        public ICollection<SelectListItem> SlotsAvailable { get; set; }

        public ICollection<SelectListItem> TaskTypesAvailable { get; set; }

        public ICollection<SelectListItem> PersonsAvailable { get; set; }

    }
}