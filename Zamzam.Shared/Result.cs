using Zamzam.Shared.Interfaces;

namespace Zamzam.Shared
{
    public class Result<T> : IResult<T>
    {
        public List<string> Message { get; set; } = new();
        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public Exception Exception { get; set; }
        public int Code { get; set; }

        #region Non async methods
        #region Success methods
        public static Result<T> Success() => new()
        {
            Succeeded = true
        };
        public static Result<T> Success(string message) => new()
        {
            Succeeded = true,
            Message = new List<string> { message }
        };

        public static Result<T> Success(T data) => new()
        {
            Succeeded = true,
            Data = data
        };
        public static Result<T> Success(T data, string message) => new()
        {
            Succeeded = true,
            Data = data,
            Message = new List<string> { message }
        };

        #endregion
        #region Failure method
        public static Result<T> Failure() => new()
        {
            Succeeded = false
        };
        public static Result<T> Failure(string message) => new()
        {
            Succeeded = false,
            Message = new List<string> { message }
        };

        public static Result<T> Failure(List<string> messages) => new()
        {
            Succeeded = false,
            Message = messages
        };

        public static Result<T> Failure(T data) => new()
        {
            Succeeded = false,
            Data = data
        };

        public static Result<T> Failure(T data, string message) => new()
        {
            Succeeded = false,
            Message = new List<string> { message },
            Data = data
        };

        public static Result<T> Failure(T data, List<string> messages) => new()
        {
            Succeeded = false,
            Message = messages,
            Data = data
        };

        public static Result<T> Failure(Exception exception) => new()
        {
            Succeeded = false,
            Exception = exception
        };
        #endregion
        #endregion

        #region Async method
        #region Succeeded methods
        public static Task<Result<T>> SuccessAsync() => Task.FromResult(Success());
        public static Task<Result<T>> SuccessAsync(string message) => Task.FromResult(Success(message));
        public static Task<Result<T>> SuccessAsync(T data) => Task.FromResult(Success(data));
        public static Task<Result<T>> SuccessAsync(T data, string message) => Task.FromResult(Success(data, message));
        #endregion
        #region Failure methods
        public static Task<Result<T>> FailureAsync() => Task.FromResult(Failure());
        public static Task<Result<T>> FailureAsync(string message) => Task.FromResult(Failure(message));
        public static Task<Result<T>> FailureAsync(T data) => Task.FromResult(Failure(data));
        public static Task<Result<T>> FailureAsync(T data, string message) => Task.FromResult(Failure(data, message));
        public static Task<Result<T>> FailureAsync(Exception exception) => Task.FromResult(Failure(exception));
        #endregion
        #endregion
    }
}

