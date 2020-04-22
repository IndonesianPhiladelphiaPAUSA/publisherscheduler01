using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublisherScheduler01Web.DataObjects;
using PublisherScheduler01Web.Repositories;

namespace PublisherScheduler01Web.ViewModels
{
    public class TaskTypeViewModel
    {
        public TaskType TaskTypeDetail { get; set; }
        public string[] RolesSelected { get; set; }
        public string[] RoleNamesSelected { get; set; }
        public ICollection<SelectListItem> Roles { get; set; }
    }
}
