using NMCK3.Shared.Exceptions;

namespace NMCK3.Domain.Exceptions
{
    public class NullParticipantEmailException:Mock3Exception
    {
        public NullParticipantEmailException() : base("Participant Email cannot be empty.")
        {
        }
    }
}
