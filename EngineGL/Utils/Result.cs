using System;

namespace EngineGL.Utils
{
    public class Result<T>
    {
        public bool IsSuccess { get; }

        private readonly T _value;

        public T Value
        {
            get
            {
                if (IsSuccess)
                    return _value;
                else
                    throw new InvalidOperationException("Can not operate because the value is null.");
            }
        }

        public string Message { get; private set; }

        public static Result<T> Success(T value)
        {
            return Success(value, string.Empty);
        }

        public static Result<T> Success(T value, string message)
        {
            return new Result<T>(value, message);
        }

        public static Result<T> Fail()
        {
            return Fail(string.Empty);
        }

        public static Result<T> Fail(string message)
        {
            return new Result<T>(message);
        }

        private Result() : this(string.Empty)
        {
        }

        private Result(string message)
        {
            IsSuccess = false;
            Message = message;
        }

        private Result(T value) : this(value, string.Empty)
        {
        }

        private Result(T value, string message)
        {
            _value = value;
            IsSuccess = value != null;
            Message = message;
        }
    }
}