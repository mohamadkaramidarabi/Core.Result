namespace Core.Result.Test;

public class ResultTests
{
    [Fact]
    public void InitSuccessReturnsSuccessBuilder()
    {
        var builder = Result<decimal, SampleSuccessStatus, SampleFailureStatus>
            .InitSuccess(SampleSuccessStatus.Completed);

        builder.ShouldBeAssignableTo<IConfigureSuccessResultBuilder<decimal, SampleSuccessStatus, SampleFailureStatus>>();
    }

    [Fact]
    public void InitFailureReturnsFailureBuilder()
    {
        var builder = Result<decimal, SampleSuccessStatus, SampleFailureStatus>
            .InitFailure(SampleFailureStatus.ValidationFailed);

        builder.ShouldBeAssignableTo<IConfigureFailureResultBuilder<decimal, SampleSuccessStatus, SampleFailureStatus>>();
    }

    [Fact]
    public void MessageDefaultsToNull()
    {
        var result = Result<bool, SampleSuccessStatus, SampleFailureStatus>
            .InitSuccess(SampleSuccessStatus.Completed)
            .WithData(true)
            .Build();

        result.Message.ShouldBeNull();
    }
}
