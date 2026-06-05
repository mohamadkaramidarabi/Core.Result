namespace Core.Result.Test;

public class SuccessTests
{
    [Fact]
    public void InitSuccessReturnsConfigurableBuilder()
    {
        IConfigureSuccessResultBuilder<string> builder = Result<string>.InitSuccess();

        builder.ShouldNotBeNull();
    }

    [Fact]
    public void BuildWithoutInitializingSuccessThrowsInvalidOperationException()
    {
        var builder = new SuccessResultBuilder<string>();

        Should.Throw<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void FluentBuilderBuildsSuccessWithDataAndMessage()
    {
        const string data = "payload";
        const string message = "completed";

        var result = Result<string>.InitSuccess()
            .WithData(data)
            .WithMessage(message)
            .Build();

        result.Data.ShouldBe(data);
        result.Message.ShouldBe(message);
    }

    [Fact]
    public void FluentBuilderAllowsNullData()
    {
        var result = Result<string?>.InitSuccess()
            .WithData(null)
            .Build();

        result.Data.ShouldBeNull();
    }

    [Fact]
    public void WithMessageBeforeSuccessDoesNotThrow()
    {
        var builder = new SuccessResultBuilder<int>();

        var configured = builder.WithMessage("early message");

        configured.ShouldBeSameAs(builder);
        Should.Throw<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void SuccessInitCreatesBuilderReadyForConfiguration()
    {
        var result = Success<int>.Init()
            .WithData(99)
            .Build();

        result.Data.ShouldBe(99);
    }
}
