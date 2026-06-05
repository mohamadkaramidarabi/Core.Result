namespace Core.Result.Test;

public class ResultBuilderTests
{
    [Fact]
    public void SuccessBuilderWithDataReturnsSameBuilderInstance()
    {
        var builder = new SuccessResultBuilder<string, SampleSuccessStatus, SampleFailureStatus>();
        builder.Success(SampleSuccessStatus.Completed);

        var configured = builder.WithData("value");

        configured.ShouldBeSameAs(builder);
    }

    [Fact]
    public void SuccessBuilderWithMessageReturnsSameBuilderInstance()
    {
        var builder = new SuccessResultBuilder<string, SampleSuccessStatus, SampleFailureStatus>();
        builder.Success(SampleSuccessStatus.Completed);

        var configured = builder.WithMessage("done");

        configured.ShouldBeSameAs(builder);
    }

    [Fact]
    public void FailureBuilderWithMessageReturnsSameBuilderInstance()
    {
        var builder = new FailureResultBuilder<int, SampleSuccessStatus, SampleFailureStatus>();
        builder.Failure(SampleFailureStatus.ValidationFailed);

        var configured = builder.WithMessage("failed");

        configured.ShouldBeSameAs(builder);
    }

    [Fact]
    public void FailureBuilderAppendErrorsReturnsSameBuilderInstance()
    {
        var builder = new FailureResultBuilder<int, SampleSuccessStatus, SampleFailureStatus>();
        builder.Failure(SampleFailureStatus.ValidationFailed);

        var configured = builder.AppendErrors(["one"]);

        configured.ShouldBeSameAs(builder);
    }

    [Fact]
    public void SuccessBuilderCanChainConfigurationCalls()
    {
        var result = new SuccessResultBuilder<string, SampleSuccessStatus, SampleFailureStatus>()
            .Success(SampleSuccessStatus.Processed)
            .WithData("alpha")
            .WithMessage("beta")
            .Build();

        result.Data.ShouldBe("alpha");
        result.Message.ShouldBe("beta");
        result.Status.ShouldBe(SampleSuccessStatus.Processed);
    }

    [Fact]
    public void FailureBuilderCanChainConfigurationCalls()
    {
        var result = new FailureResultBuilder<string, SampleSuccessStatus, SampleFailureStatus>()
            .Failure(SampleFailureStatus.NotFound)
            .WithMessage("gamma")
            .AppendErrors(["delta"])
            .Build();

        result.Message.ShouldBe("gamma");
        result.Errors.ShouldBe(["delta"]);
        result.Status.ShouldBe(SampleFailureStatus.NotFound);
    }
}
