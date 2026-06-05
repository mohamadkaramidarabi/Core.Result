
namespace Core.Result
{

    internal class SuccessResultBuilder<T> :
        IInitialSuccessResultBuilder<T>,
        IConfigureSuccessResultBuilder<T>
    {
        internal SuccessResultBuilder() { }
        private Success<T>? _successResult;
        public Success<T> Build()
        {
            return _successResult ?? throw new InvalidOperationException("Success result with data is not initialized.");
        }
        public IConfigureSuccessResultBuilder<T> WithMessage(string? message)
        {
            _successResult?.Message = message;
            return this;
        }


        public IConfigureSuccessResultBuilder<T> WithData(T? data)
        {
            _successResult?.Data = data;
            return this;
        }

        public IConfigureSuccessResultBuilder<T> Success()
        {
            _successResult = new Success<T>();
            return this;
        }
    }
    internal class  FailureResultBuilder<T> :
        IInitialFailureResultBuilder<T>,
        IConfigureFailureResultBuilder<T>
    {
        internal FailureResultBuilder() { }
        private Failure<T>? _failureResult;

        public IConfigureFailureResultBuilder<T> AppendErrors(List<string> errors)
        {
            _failureResult?.AppendError(errors);
            return this;
        }

        public Failure<T> Build()
        {
            return _failureResult ?? throw new InvalidOperationException("Failure result is not initialized.");
        }

        public IConfigureFailureResultBuilder<T> Failure()
        {
            _failureResult = new Failure<T>();
            return this;
        }
        public IConfigureFailureResultBuilder<T> WithMessage(string message)
        {
            _failureResult!.Message = message;
            return this;
        }

    }
}
