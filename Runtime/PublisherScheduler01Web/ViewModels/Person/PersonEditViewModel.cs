using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublisherScheduler01Web.Repositories;

namespace PublisherScheduler01Web.ViewModels.Person
{
    public class PersonEditViewModel
    {
        ISchedulerRepository _repository;

        public PersonEditViewModel(ISchedulerRepository repository)
        {
            _repository = repository;
        }

        public DataObjects.Person PersonDetail { get; set; }

        public string[] RolesSelected
        {
            get
            {
                List<string> roles = new List<string>();

                foreach(var role in PersonDetail.Capacities)
                {
                    roles.Add(role.Id.ToString());
                }

                return roles.ToArray();
            }
        }

        public ICollection<SelectListItem> Capacities
        {
            get
            {
                var allRoles = _repository.GetRoles().ToList();

                List<SelectListItem> selectListItems = new List<SelectListItem>();

                if (allRoles != null)
                {
                    foreach (var role in allRoles)
                    {
                        selectListItems.Add(new SelectListItem { Text = role.Name, Value = role.Id.ToString() });
                    }
                    
                }

                return selectListItems;
            }
        }
    }
}