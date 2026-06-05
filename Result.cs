namespace Core.Result
{
    /// <summary>
    /// Represents the outcome of an operation that can succeed or fail.
    /// </summary>
    /// <typeparam name="T">The type of the success payload.</typeparam>
    public abstract class Result<T>
    {
        /// <summary>
        /// Optional message describing the outcome.
        /// </summary>
        public string? Message { get; internal set; } = null;

        internal Result() { }

        /// <summary>
        /// Starts building a success result.
        /// </summary>
        public static IConfigureSuccessResultBuilder<T> InitSuccess() => Success<T>.Init();

        /// <summary>
        /// Starts building a failure result.
        /// </summary>
        public static IConfigureFailureResultBuilder<T> InitFailure() => Failure<T>.Init();
    }

    /// <summary>
    /// Represents a successful operation with an optional payload.
    /// </summary>
    /// <typeparam name="T">The type of the success payload.</typeparam>
    public class Success<T> : Result<T>
    {
        /// <summary>
        /// Starts building a success result.
        /// </summary>
        public static IConfigureSuccessResultBuilder<T> Init() => (new SuccessResultBuilder<T>()).Success();

        /// <summary>
        /// The success payload.
        /// </summary>
        public T? Data { get; internal set; }

        internal Success()
        {
            Data = default(T);
        }
    }

    /// <summary>
    /// Represents a failed operation with a list of error messages.
    /// </summary>
    /// <typeparam name="T">The type that would have been returned on success.</typeparam>
    public sealed class Failure<T> : Result<T>
    {
        /// <summary>
        /// Starts building a failure result.
        /// </summary>
        public static IConfigureFailureResultBuilder<T> Init() => (new FailureResultBuilder<T>()).Failure();

        /// <summary>
        /// The error messages associated with the failure.
        /// </summary>
        public List<string> Errors { get; private set; } = [];

        /// <summary>
        /// Appends error messages to the failure.
        /// </summary>
        /// <param name="errors">The errors to append.</param>
        public void AppendError(List<string> errors)
        {
            Errors.AddRange(errors);
        }

        internal Failure()
        {
        }
    }
}
