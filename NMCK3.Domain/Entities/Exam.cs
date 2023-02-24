using NMCK3.Domain.Exceptions;
using NMCK3.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace NMCK3.Domain.Entities
{
    public sealed class Exam : Entity
    {
        private readonly List<ExamReservation> _examReservations = new();

        public string Name { get; private set; }
        public string ExamDate { get; private set; }
        public string Description { get; private set; }
        public int Capacity { get; private set; }
        public int RemainingCapacity { get; private set; }
        public bool IsOpen { get; private set; }
        public IReadOnlyCollection<ExamReservation> ExamReservations => _examReservations;


        private Exam(Guid id, string name, string examDate, string description, int capacity) 
            : base(id)
        {
            Name = name;
            ExamDate = examDate;
            Description = description;
            Capacity = capacity;
            RemainingCapacity = capacity;
            IsOpen = true;
        }


        public static Exam Create(string name, string examDate, string description, int capacity)
        {
            if (string.IsNullOrEmpty(name.Trim()))
                throw new EmptyExamNameException();

            if (examDate == null)
                throw new NullExamDateException();

            var exam = new Exam(Guid.NewGuid(), name, examDate, description, capacity);
            return exam;
        }

        public void Update(string name, string examDate, string description, int capacity)
        {
            if (string.IsNullOrEmpty(name.Trim()))
                throw new EmptyExamNameException();

            if (examDate == null)
                throw new NullExamDateException();

            Name = name;
            ExamDate = examDate;
            Description = description;
            Capacity = capacity;
        }

        public void OpenRegistration()
        {
            IsOpen = true;
        }

        public void CloseRegistration()
        {
            IsOpen = false;
        }

        public ExamReservation SubmitReservation(Participant participant)
        {
            if (participant is null)
                throw new NullParticipantException();

            var reservation = new ExamReservation(Guid.NewGuid(), participant, this);

            _examReservations.Add(reservation);

            return reservation;
        }

    }
}
