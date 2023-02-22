using System;
using System.Globalization;

namespace NMCK3.Domain
{
    public class Exam
    {


        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public PersianCalendar StartDate { get; private set; }
        public string Description { get; private set; }
        public int Capacity { get; private set; }
        public int RemainingCapacity { get; private set; }
        public bool IsOpen { get; private set; }

        private Exam(Guid id, string name, PersianCalendar startDate, string description, int capacity)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            Description = description;
            Capacity = capacity;
            RemainingCapacity = capacity;
            IsOpen = true;
        }

        public static Exam Create(Guid id, string name, PersianCalendar startDate, string description, int capacity)
        {
            var exam = new Exam(Guid.NewGuid(), name, startDate, description, capacity);
            return exam;
        }

    }
}
