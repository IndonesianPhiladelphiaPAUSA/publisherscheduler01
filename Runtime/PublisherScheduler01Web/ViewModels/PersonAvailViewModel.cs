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
    public class PersonAvailViewModel
    {
        public DataObjects.PersonAvail PersonAvailDetail { get; set; }

        [Display(Name = "Person: ")]
        public string PersonName { get; set; }

        public ICollection<SelectListItem> PersonList { get; set; }
    }
}