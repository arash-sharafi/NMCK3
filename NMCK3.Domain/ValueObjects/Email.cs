using NMCK3.Domain.Errors;
using NMCK3.Domain.Primitives;
using NMCK3.Domain.Shared;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NMCK3.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; }
        private Email(string value)
        {
            Value = value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

        public static Result<Email> Create(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                Result.Fail<Email>(DomainErrors.Email.NullOrEmptyEmail);

            if (IsEmailValid(emailAddress))
                Result.Fail<Email>(DomainErrors.Email.InvalidEmail);

            return new Email(emailAddress);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static bool IsEmailValid(string emailString)
        {
            var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);

            return emailRegex.IsMatch(emailString);
        }
    }
}
