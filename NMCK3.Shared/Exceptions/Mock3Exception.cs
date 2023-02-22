using System;

namespace NMCK3.Shared.Exceptions
{
    public abstract class Mock3Exception : Exception
    {
        protected Mock3Exception(string message) : base(message)
        {

        }
    }
}
