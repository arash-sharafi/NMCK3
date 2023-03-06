using NMCK3.Domain.Primitives;
using NMCK3.Domain.Shared;
using NMCK3.Domain.ValueObjects;
using System;

namespace NMCK3.Domain.Entities
{
    public sealed class User : AggregateRoot
    {
        private User(string id, Email email)
            : base(Guid.Parse(id))
        {
            Id = id;
            Email = email;
        }

        public new string Id { get; }

        public Email Email { get; }

        public static Result<User> Create(string id, string email)
        {
            var emailResult = Email.Create(email);

            if (emailResult.IsFailure)
                return Result.Fail<User>(emailResult.Error);

            var participant = new User(id, emailResult.Value);

            return participant;
        }

        private User()
        {

        }
    }
}
