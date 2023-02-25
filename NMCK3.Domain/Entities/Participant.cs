using NMCK3.Domain.Common;
using NMCK3.Domain.Primitives;
using NMCK3.Domain.ValueObjects;
using System;

namespace NMCK3.Domain.Entities
{
    public sealed class Participant : AggregateRoot
    {
        private Participant(Guid id, ParticipantEmail email)
            : base(id)
        {
            Email = email;
        }

        public ParticipantEmail Email { get; private set; }

        public static Result<Participant> Create(ParticipantEmail email)
        {
            var participant = new Participant(Guid.NewGuid(), email);

            return participant;
        }
    }
}
