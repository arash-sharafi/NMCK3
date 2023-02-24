using NMCK3.Domain.Common;
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

        public static Result<Participant> Create(string email)
        {
            

            var participant = new Participant(Guid.NewGuid(), email);

            return participant;
        }
    }
}
