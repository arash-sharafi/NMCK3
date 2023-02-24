using NMCK3.Domain.Exceptions;
using NMCK3.Domain.Primitives;
using System;

namespace NMCK3.Domain.Entities
{
    public sealed class Participant : Entity
    {
        private Participant(Guid id, string email)
            : base(id)
        {
            Email = email;
        }

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
