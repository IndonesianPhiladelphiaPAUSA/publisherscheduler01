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
        ISchedulerRepository _repository;

        public AssignmentViewModel(ISchedulerRepository repository)
        {
            _repository = repository;
        }

        public Assignment AssignmentDetail { get; set; }

        [Display(Name = "Slot")]
        public string SlotSelected
        {
            get
            {
                Slot slot = _repository.GetSlotById(AssignmentDetail.SlotId);
                return slot == null ? "" : slot.Name;
            }
        }

        [Display(Name = "Task Type")]
        public string TaskTypeSelected
        {
            get
            {
                TaskType taskType = _repository.GetTaskTypeById(AssignmentDetail.TaskTypeId);
                return taskType == null ? "" : taskType.Name;
            }
        }

        [Display(Name = "Person")]
        public string PersonSelected
        {
            get
            {
                Person person = _repository.GetPersonById(AssignmentDetail.PersonId);
                return person == null ? "" : person.Name;
            }
        }
        
        public ICollection<SelectListItem> SlotsAvailable
        {
            get
            {
                var allSlots = _repository.GetSlots().ToList();

                List<SelectListItem> selectListItems = new List<SelectListItem>();

                if (allSlots != null)
                {
                    foreach (var slot in allSlots)
                    {
                        selectListItems.Add(new SelectListItem { Text = slot.Name, Value = slot.Id.ToString() });
                    }
                }

                return selectListItems;
            }
        }

        public ICollection<SelectListItem> TaskTypesAvailable
        {
            get
            {
                var allTaskTypes = _repository.GetTaskTypes().ToList();

                List<SelectListItem> selectListItems = new List<SelectListItem>();

                if (allTaskTypes != null)
                {
                    foreach (var taskType in allTaskTypes)
                    {
                        selectListItems.Add(new SelectListItem { Text = taskType.Name, Value = taskType.Id.ToString() });
                    }
                }

                return selectListItems;
            }
        }

        public ICollection<SelectListItem> PersonsAvailable
        {
            get
            {
                var allPersons = _repository.GetPersons().ToList();

                List<SelectListItem> selectListItems = new List<SelectListItem>();

                if (allPersons != null)
                {
                    foreach (var person in allPersons)
                    {
                        selectListItems.Add(new SelectListItem { Text = person.Name, Value = person.Id.ToString() });
                    }
                }

                return selectListItems;
            }
        }

    }
}