namespace Core.Result
{
    /// <summary>
    /// Marker interface for initial result builder entry points.
    /// </summary>
    public interface IInitialServiceResultBuilder
    {
    }

    /// <summary>
    /// Starts configuration of a success result.
    /// </summary>
    /// <typeparam name="T">The type of the success payload.</typeparam>
    public interface IInitialSuccessResultBuilder<T> : IInitialServiceResultBuilder
    {
        /// <summary>
        /// Initializes a success result for further configuration.
        /// </summary>
        IConfigureSuccessResultBuilder<T> Success();
    }

    /// <summary>
    /// Starts configuration of a failure result.
    /// </summary>
    /// <typeparam name="T">The type that would have been returned on success.</typeparam>
    public interface IInitialFailureResultBuilder<T> : IInitialServiceResultBuilder
    {
        /// <summary>
        /// Initializes a failure result for further configuration.
        /// </summary>
        IConfigureFailureResultBuilder<T> Failure();
    }
}
