namespace Core.Result
{
    public abstract class Result<T, TSuccessStatus, TFailureStatus>
        where TSuccessStatus : struct, Enum
        where TFailureStatus : struct, Enum
    {
        public string? Message { get; internal set; } = null;

        internal Result() { }

        public static IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus> InitSuccess(TSuccessStatus status)
            => Success<T, TSuccessStatus, TFailureStatus>.Init(status);

        public static IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus> InitFailure(TFailureStatus status)
            => Failure<T, TSuccessStatus, TFailureStatus>.Init(status);
    }

    public class Success<T, TSuccessStatus, TFailureStatus> : Result<T, TSuccessStatus, TFailureStatus>
        where TSuccessStatus : struct, Enum
        where TFailureStatus : struct, Enum
    {
        public static IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus> Init(TSuccessStatus status)
            => (new SuccessResultBuilder<T, TSuccessStatus, TFailureStatus>()).Success(status);

        public TSuccessStatus Status { get; internal set; }

        public T? Data { get; internal set; }

        internal Success()
        {
            Data = default(T);
        }
    }

    public sealed class Failure<T, TSuccessStatus, TFailureStatus> : Result<T, TSuccessStatus, TFailureStatus>
        where TSuccessStatus : struct, Enum
        where TFailureStatus : struct, Enum
    {
        public static IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus> Init(TFailureStatus status)
            => (new FailureResultBuilder<T, TSuccessStatus, TFailureStatus>()).Failure(status);

        public TFailureStatus Status { get; internal set; }

        public List<string> Errors { get; private set; } = [];

        public void AppendError(List<string> errors)
        {
            Errors.AddRange(errors);
        }

        internal Failure()
        {
        }
    }
}
