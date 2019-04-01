using System;

namespace PublisherScheduler01Data.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime TimeBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime TimeEnd { get; set; }
        public int TaskId { get; set; }
        public int LocationId { get; set; }
        public bool IsActive { get; set; }

    }
}