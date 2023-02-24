using System;

namespace NMCK3.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }
        public bool IsFailure => !IsSuccess;


        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;

        }

        public static Result Fail(Error error) => new(false, error);
        public static Result<T> Fail<T>(Error error) => new(default(T), false, error);


        public static Result Success() => new(true, Error.None);
        public static Result<T> Success<T>(T value) => new(value, true, Error.None);

        public static Result<T> Create<T>(T value) => Success(value);

    }
}
