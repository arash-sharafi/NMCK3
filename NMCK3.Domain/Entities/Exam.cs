using NMCK3.Domain.Common;
using NMCK3.Domain.Errors;
using NMCK3.Domain.Exceptions;
using NMCK3.Domain.Primitives;
using NMCK3.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace NMCK3.Domain.Entities
{
    public sealed class Exam : AggregateRoot
    {
        private readonly List<ExamReservation> _examReservations = new();

        public string Name { get; private set; }
        public ExamDate ExamDate { get; private set; }
        public string Description { get; private set; }
        public int Capacity { get; private set; }
        public int RemainingCapacity { get; private set; }
        public bool IsOpen { get; private set; }
        public IReadOnlyCollection<ExamReservation> ExamReservations => _examReservations;


        private Exam(Guid id, string name, ExamDate examDate, string description, int capacity)
            : base(id)
        {
            Name = name;
            ExamDate = examDate;
            Description = description;
            Capacity = capacity;
            RemainingCapacity = capacity;
            IsOpen = true;
        }


        public static Result<Exam> Create(string name, ExamDate examDate, string description, int capacity)
        {
            if (string.IsNullOrEmpty(name.Trim()))
                return Result.Fail<Exam>(DomainErrors.Exam.EmptyName);

            if (examDate == null)
                throw new NullExamDateException();

            var exam = new Exam(Guid.NewGuid(), name, examDate, description, capacity);
            return exam;
        }

        public Result Update(string name, ExamDate examDate, string description, int capacity)
        {
            if (string.IsNullOrEmpty(name.Trim()))
                return Result.Fail<Exam>(DomainErrors.Exam.EmptyName);

            if (examDate == null)
                throw new NullExamDateException();

            Name = name;
            ExamDate = examDate;
            Description = description;
            Capacity = capacity;

            return Result.Success();
        }

        public Result OpenRegistration()
        {
            IsOpen = true;
            return Result.Success();
        }

        public Result CloseRegistration()
        {
            IsOpen = false;
            return Result.Success();
        }

        public Result<ExamReservation> SubmitReservation(User participant)
        {
            if (participant is null)
                return Result.Fail<ExamReservation>(DomainErrors.ExamReservation.NullParticipant);

            var reservation = new ExamReservation(Guid.NewGuid(), participant, this);

            _examReservations.Add(reservation);

            return reservation;
        }

    }
}
