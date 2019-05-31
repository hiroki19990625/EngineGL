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
        /// ResultがSuccessの状態ならば、引数で受け取った関数にvalueを渡し、変換したResultを返します
        /// ResultがFailの状態ならば、そのままFailのResultを返します
        /// </summary>
        /// <returns>
        /// 引数fで変換後のResultを返します
        /// </returns>
        public Result<R> Then<R>(Func<T, Result<R>> f)
            => IsSuccess ? f(_value) : Result<R>.Fail();

        /// <summary>
        /// ResultがFailの状態ならば、引数で受け取った関数を呼び出し、呼び出し結果のResultを返します
        /// ResultがSuccessの状態ならば、そのままSuccessのResultを返します
        /// </summary>
        /// <returns>
        /// 引数fで変換後のResultを返します
        /// </returns>
        public Result<T> Catch<R>(Func<Result<T>> f)
            => IsSuccess ? this : f();

        /// <summary>
        /// Resultを変換します
        /// ResultがSuccessの状態ならば、successFuncで変換します
        /// ResultがFailの状態ならば、failFuncで変換します
        /// </summary>
        /// <returns>
        /// 変換した値を返します
        /// </returns>
        public R Match<R>(Func<T, R> successFunc, Func<R> failFunc)
            => IsSuccess ? successFunc(_value) : failFunc();

        /// <summary>
        /// ResultがSuccessの状態ならば、内部のvalueを返し
        /// ResultがFailの状態ならば、引数で受け取った値をそのまま返します
        /// </summary>
        /// <returns>
        /// 内部のvalueもしくは引数で受け取った値
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