namespace Core.Result.Test;

public class FailureTests
{
    [Fact]
    public void InitFailureReturnsConfigurableBuilder()
    {
        IConfigureFailureResultBuilder<int> builder = Result<int>.InitFailure();

        builder.ShouldNotBeNull();
    }

    [Fact]
    public void BuildWithoutInitializingFailureThrowsInvalidOperationException()
    {
        var builder = new FailureResultBuilder<string>();

        Should.Throw<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void FluentBuilderBuildsFailureWithMessageAndErrors()
    {
        const string message = "validation failed";
        var errors = new List<string> { "field required", "invalid format" };

        var result = Result<string>.InitFailure()
            .WithMessage(message)
            .AppendErrors(errors)
            .Build();

        result.Message.ShouldBe(message);
        result.Errors.ShouldBe(errors);
    }

    [Fact]
    public void AppendErrorAddsErrorsToFailure()
    {
        var failure = new Failure<int>();
        var firstBatch = new List<string> { "error-a" };
        var secondBatch = new List<string> { "error-b", "error-c" };

        failure.AppendError(firstBatch);
        failure.AppendError(secondBatch);

        failure.Errors.ShouldBe(["error-a", "error-b", "error-c"]);
    }

    [Fact]
    public void FailureInitCreatesBuilderReadyForConfiguration()
    {
        var result = Failure<string>.Init()
            .WithMessage("not found")
            .AppendErrors(["missing resource"])
            .Build();

        result.Message.ShouldBe("not found");
        result.Errors.ShouldHaveSingleItem();
        result.Errors[0].ShouldBe("missing resource");
    }
}
