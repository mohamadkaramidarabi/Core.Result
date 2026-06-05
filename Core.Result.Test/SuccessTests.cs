namespace Core.Result.Test;

public class SuccessTests
{
    [Fact]
    public void InitSuccessReturnsConfigurableBuilder()
    {
        IConfigureSuccessResultBuilder<string, SampleSuccessStatus, SampleFailureStatus> builder =
            Result<string, SampleSuccessStatus, SampleFailureStatus>
                .InitSuccess(SampleSuccessStatus.Completed);

        builder.ShouldNotBeNull();
    }

    [Fact]
    public void BuildWithoutInitializingSuccessThrowsInvalidOperationException()
    {
        var builder = new SuccessResultBuilder<string, SampleSuccessStatus, SampleFailureStatus>();

        Should.Throw<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void FluentBuilderBuildsSuccessWithDataMessageAndStatus()
    {
        const string data = "payload";
        const string message = "completed";

        var result = Result<string, SampleSuccessStatus, SampleFailureStatus>
            .InitSuccess(SampleSuccessStatus.Processed)
            .WithData(data)
            .WithMessage(message)
            .Build();

        result.Data.ShouldBe(data);
        result.Message.ShouldBe(message);
        result.Status.ShouldBe(SampleSuccessStatus.Processed);
    }

    [Fact]
    public void FluentBuilderAllowsNullData()
    {
        var result = Result<string?, SampleSuccessStatus, SampleFailureStatus>
            .InitSuccess(SampleSuccessStatus.Completed)
            .WithData(null)
            .Build();

        result.Data.ShouldBeNull();
    }

    [Fact]
    public void WithMessageBeforeSuccessDoesNotThrow()
    {
        var builder = new SuccessResultBuilder<int, SampleSuccessStatus, SampleFailureStatus>();

        var configured = builder.WithMessage("early message");

        configured.ShouldBeSameAs(builder);
        Should.Throw<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void SuccessInitCreatesBuilderReadyForConfiguration()
    {
        var result = Success<int, SampleSuccessStatus, SampleFailureStatus>
            .Init(SampleSuccessStatus.Completed)
            .WithData(99)
            .Build();

        result.Data.ShouldBe(99);
        result.Status.ShouldBe(SampleSuccessStatus.Completed);
    }
}
