namespace Core.Result.Test;

public class ResultTests
{
    [Fact]
    public void InitSuccessReturnsSuccessBuilder()
    {
        var builder = Result<decimal>.InitSuccess();

        builder.ShouldBeAssignableTo<IConfigureSuccessResultBuilder<decimal>>();
    }

    [Fact]
    public void InitFailureReturnsFailureBuilder()
    {
        var builder = Result<decimal>.InitFailure();

        builder.ShouldBeAssignableTo<IConfigureFailureResultBuilder<decimal>>();
    }

    [Fact]
    public void MessageDefaultsToNull()
    {
        var result = Result<bool>.InitSuccess()
            .WithData(true)
            .Build();

        result.Message.ShouldBeNull();
    }
}
