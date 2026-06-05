namespace Core.Result.Test;

public class ResultBuilderTests
{
    [Fact]
    public void SuccessBuilderWithDataReturnsSameBuilderInstance()
    {
        var builder = new SuccessResultBuilder<string>();
        builder.Success();

        var configured = builder.WithData("value");

        configured.ShouldBeSameAs(builder);
    }

    [Fact]
    public void SuccessBuilderWithMessageReturnsSameBuilderInstance()
    {
        var builder = new SuccessResultBuilder<string>();
        builder.Success();

        var configured = builder.WithMessage("done");

        configured.ShouldBeSameAs(builder);
    }

    [Fact]
    public void FailureBuilderWithMessageReturnsSameBuilderInstance()
    {
        var builder = new FailureResultBuilder<int>();
        builder.Failure();

        var configured = builder.WithMessage("failed");

        configured.ShouldBeSameAs(builder);
    }

    [Fact]
    public void FailureBuilderAppendErrorsReturnsSameBuilderInstance()
    {
        var builder = new FailureResultBuilder<int>();
        builder.Failure();

        var configured = builder.AppendErrors(["one"]);

        configured.ShouldBeSameAs(builder);
    }

    [Fact]
    public void SuccessBuilderCanChainConfigurationCalls()
    {
        var result = new SuccessResultBuilder<string>()
            .Success()
            .WithData("alpha")
            .WithMessage("beta")
            .Build();

        result.Data.ShouldBe("alpha");
        result.Message.ShouldBe("beta");
    }

    [Fact]
    public void FailureBuilderCanChainConfigurationCalls()
    {
        var result = new FailureResultBuilder<string>()
            .Failure()
            .WithMessage("gamma")
            .AppendErrors(["delta"])
            .Build();

        result.Message.ShouldBe("gamma");
        result.Errors.ShouldBe(["delta"]);
    }
}
