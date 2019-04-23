using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PublisherScheduler01Web.DataObjects;
using PublisherScheduler01Web.Repositories;

namespace PublisherScheduler01Web.ViewModels
{
    public class PersonViewModel
    {
        ISchedulerRepository _repository;

        public PersonViewModel(ISchedulerRepository repository)
        {
            _repository = repository;
        }

        public Person PersonDetail { get; set; }

        public string[] RolesSelected
        {
            get
            {
                List<string> roles = new List<string>();

                if (PersonDetail.Capacities != null)
                {
                    foreach (var role in PersonDetail.Capacities)
                    {
                        int? id = role.Id;
                        roles.Add(id == null ? "" : role.Id.ToString());
                    }
                }

                return roles.ToArray();
            }
        }

        public string[] RoleNamesSelected
        {
            get
            {
                List<string> roles = new List<string>();

                foreach (var role in PersonDetail.Capacities)
                {
                    roles.Add(role.Name);
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