namespace Core.Result
{
    public interface IConfigureResultBuilder;

    public interface IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus> : IConfigureResultBuilder
        where TSuccessStatus : struct, Enum
        where TFailureStatus : struct, Enum
    {
        IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus> WithMessage(string? message);

        IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus> WithData(T? data);

        Success<T, TSuccessStatus, TFailureStatus> Build();
    }

    public interface IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus> : IConfigureResultBuilder
        where TSuccessStatus : struct, Enum
        where TFailureStatus : struct, Enum
    {
        IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus> AppendErrors(List<string> errors);

        IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus> WithMessage(string message);

        Failure<T, TSuccessStatus, TFailureStatus> Build();
    }
}
