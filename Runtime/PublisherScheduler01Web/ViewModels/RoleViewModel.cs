using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublisherScheduler01Web.DataObjects;
using PublisherScheduler01Web.Repositories;

namespace PublisherScheduler01Web.ViewModels
{
    public class RoleViewModel
    {
        public Capacity RoleDetail { get; set; }
        public string Name { get; set; }

    }
}
