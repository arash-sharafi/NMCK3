using NMCK3.Domain.Primitives;
using System;

namespace NMCK3.Domain.Entities
{
    public sealed class ExamReservation : Entity
    {
        internal ExamReservation(Guid id, Participant participant, Exam exam)
            : base(id)
        {
            ParticipantId = participant.Id;
            ExamId = exam.Id;
        }


        public double ReadingScore { get; private set; } = 0.0;

        public double ListeningScore { get; private set; } = 0.0;

        public double SpeakingScore { get; private set; } = 0.0;

        public double WritingScore { get; private set; } = 0.0;

        public string ScoreSubmitDate { get; private set; }

        public Guid ParticipantId { get; private set; }

        public Guid ExamId { get; private set; }
    }
}
