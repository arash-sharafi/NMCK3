using System;

namespace NMCK3.Domain.Entities
{
    public class ExamReservation
    {
        internal ExamReservation(Guid id, Participant participant, Exam exam)
        {
            Id = id;
            ParticipantId = participant.Id;
            ExamId = exam.Id;
        }

        public Guid Id { get; private set; }

        public double ReadingScore { get; private set; } = 0.0;

        public double ListeningScore { get; private set; } = 0.0;

        public double SpeakingScore { get; private set; } = 0.0;

        public double WritingScore { get; private set; } = 0.0;

        public string ScoreSubmitDate { get; private set; } 

        public Guid ParticipantId { get; private set; }

        public Guid ExamId { get; private set; }
    }
}
