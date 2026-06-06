
namespace Core.Result
{

    internal class SuccessResultBuilder<T, TSuccessStatus, TFailureStatus> :
        IInitialSuccessResultBuilder<T, TSuccessStatus, TFailureStatus>,
        IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus>
        where TSuccessStatus : struct, Enum
        where TFailureStatus : struct, Enum
    {
        internal SuccessResultBuilder() { }
        private Success<T, TSuccessStatus, TFailureStatus>? _successResult;

        public Success<T, TSuccessStatus, TFailureStatus> Build()
        {
            return _successResult ?? throw new InvalidOperationException("Success result with data is not initialized.");
        }

        public IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus> WithMessage(string? message)
        {
            _successResult?.Message = message;
            return this;
        }

        public IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus> WithData(T? data)
        {
            _successResult?.Data = data;
            return this;
        }

        public IConfigureSuccessResultBuilder<T, TSuccessStatus, TFailureStatus> Success(TSuccessStatus status)
        {
            _successResult = new Success<T, TSuccessStatus, TFailureStatus> { Status = status };
            return this;
        }
    }

    internal class FailureResultBuilder<T, TSuccessStatus, TFailureStatus> :
        IInitialFailureResultBuilder<T, TSuccessStatus, TFailureStatus>,
        IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus>
        where TSuccessStatus : struct, Enum
        where TFailureStatus : struct, Enum
    {
        internal FailureResultBuilder() { }
        private Failure<T, TSuccessStatus, TFailureStatus>? _failureResult;

        public IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus> AppendErrors(List<string> errors)
        {
            _failureResult?.AppendError(errors);
            return this;
        }

        public Failure<T, TSuccessStatus, TFailureStatus> Build()
        {
            return _failureResult ?? throw new InvalidOperationException("Failure result is not initialized.");
        }

        public IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus> Failure(TFailureStatus status)
        {
            _failureResult = new Failure<T, TSuccessStatus, TFailureStatus> { Status = status };
            return this;
        }

        public IConfigureFailureResultBuilder<T, TSuccessStatus, TFailureStatus> WithMessage(string message)
        {
            _failureResult!.Message = message;
            return this;
        }
    }
}
