using NMCK3.Domain.Primitives;
using NMCK3.Domain.Shared;
using NMCK3.Domain.ValueObjects;
using System;

namespace NMCK3.Domain.Entities
{
    public sealed class User : AggregateRoot
    {
        private User(Guid id, Email email)
            : base(id)
        {
            Email = email;
        }

        public Email Email { get; private set; }

        public static Result<User> Create(Email email)
        {
            var participant = new User(Guid.NewGuid(), email);

            return participant;
        }
    }
}
