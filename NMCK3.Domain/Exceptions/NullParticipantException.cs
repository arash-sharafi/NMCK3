using NMCK3.Shared.Exceptions;

namespace NMCK3.Domain.Exceptions
{
    class NullParticipantException : Mock3Exception
    {
        public NullParticipantException() : base("Participant cannot be null")
        {
        }
    }
}
