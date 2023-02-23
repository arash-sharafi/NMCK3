using NMCK3.Domain.Exceptions;
using System;

namespace NMCK3.Domain.Entities
{
    public class Participant
    {
        private Participant(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; }

        public static Participant Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new NullParticipantEmailException();

            var participant = new Participant(Guid.NewGuid(), email);

            return participant;
        }
    }
}
