using NMCK3.Domain.Common;
using NMCK3.Domain.Errors;
using NMCK3.Domain.Primitives;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NMCK3.Domain.ValueObjects
{
    public class ParticipantEmail : ValueObject
    {
        public string Value { get; }
        private ParticipantEmail(string value)
        {
            Value = value;
        }

        public static Result<ParticipantEmail> Create(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                Result.Fail<ParticipantEmail>(DomainErrors.ParticipantEmail.NullOrEmptyEmail);

            if (IsEmailValid(emailAddress))
                Result.Fail<ParticipantEmail>(DomainErrors.ParticipantEmail.InvalidEmail);

            return new ParticipantEmail(emailAddress);
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
