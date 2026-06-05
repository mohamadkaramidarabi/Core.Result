namespace Core.Result
{
    /// <summary>
    /// Marker interface for result builders.
    /// </summary>
    public interface IConfigureResultBuilder;

    /// <summary>
    /// Configures and builds a success result.
    /// </summary>
    /// <typeparam name="T">The type of the success payload.</typeparam>
    public interface IConfigureSuccessResultBuilder<T> : IConfigureResultBuilder
    {
        /// <summary>
        /// Sets an optional message on the result.
        /// </summary>
        IConfigureSuccessResultBuilder<T> WithMessage(string? message);

        /// <summary>
        /// Sets the success payload.
        /// </summary>
        IConfigureSuccessResultBuilder<T> WithData(T? data);

        /// <summary>
        /// Builds the configured success result.
        /// </summary>
        Success<T> Build();
    }

    /// <summary>
    /// Configures and builds a failure result.
    /// </summary>
    /// <typeparam name="T">The type that would have been returned on success.</typeparam>
    public interface IConfigureFailureResultBuilder<T> : IConfigureResultBuilder
    {
        /// <summary>
        /// Appends error messages to the failure.
        /// </summary>
        IConfigureFailureResultBuilder<T> AppendErrors(List<string> errors);

        /// <summary>
        /// Sets the failure message.
        /// </summary>
        IConfigureFailureResultBuilder<T> WithMessage(string message);

        /// <summary>
        /// Builds the configured failure result.
        /// </summary>
        Failure<T> Build();
    }
}
