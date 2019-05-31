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

        /// <summary>
        /// Result��Success�̏�ԂȂ�΁A�����Ŏ󂯎�����֐���value��n���A�ϊ�����Result��Ԃ��܂�
        /// Result��Fail�̏�ԂȂ�΁A���̂܂�Fail��Result��Ԃ��܂�
        /// </summary>
        /// <returns>
        /// ����f�ŕϊ����Result��Ԃ��܂�
        /// </returns>
        public Result<R> Then<R>(Func<T, Result<R>> f)
            => IsSuccess ? f(_value) : Result<R>.Fail();

        /// <summary>
        /// Result��Fail�̏�ԂȂ�΁A�����Ŏ󂯎�����֐����Ăяo���A�Ăяo�����ʂ�Result��Ԃ��܂�
        /// Result��Success�̏�ԂȂ�΁A���̂܂�Success��Result��Ԃ��܂�
        /// </summary>
        /// <returns>
        /// ����f�ŕϊ����Result��Ԃ��܂�
        /// </returns>
        public Result<T> Catch<R>(Func<Result<T>> f)
            => IsSuccess ? this : f();

        /// <summary>
        /// Result��ϊ����܂�
        /// Result��Success�̏�ԂȂ�΁AsuccessFunc�ŕϊ����܂�
        /// Result��Fail�̏�ԂȂ�΁AfailFunc�ŕϊ����܂�
        /// </summary>
        /// <returns>
        /// �ϊ������l��Ԃ��܂�
        /// </returns>
        public R Match<R>(Func<T, R> successFunc, Func<R> failFunc)
            => IsSuccess ? successFunc(_value) : failFunc();

        /// <summary>
        /// Result��Success�̏�ԂȂ�΁A������value��Ԃ�
        /// Result��Fail�̏�ԂȂ�΁A�����Ŏ󂯎�����l�����̂܂ܕԂ��܂�
        /// </summary>
        /// <returns>
        /// ������value�������͈����Ŏ󂯎�����l
        /// </returns>
        public T Or(T t)
            => IsSuccess ? _value : t;



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