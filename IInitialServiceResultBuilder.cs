namespace Core.Result
{
    public interface IInitialServiceResultBuilder
    {
    }

    public interface IInitialSuccessResultBuilder<T, TSuccessStatus, TFailureStatus> : IInitialServiceResultBuilder
        where TSuccessStatus : struct, Enum
        where TFailureStatus : struct, Enum
    {
        IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus> Success(TSuccessStatus status);
    }

    public interface IInitialFailureResultBuilder<T, TSuccessStatus, TFailureStatus> : IInitialServiceResultBuilder
        where TSuccessStatus : struct, Enum
        where TFailureStatus : struct, Enum
    {
        IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus> Failure(TFailureStatus status);
    }
}
