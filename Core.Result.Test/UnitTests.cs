namespace Core.Result.Test;

public class UnitTests
{
    [Fact]
    public void ValueReturnsSingletonInstance()
    {
        var first = Unit.Value;
        var second = Unit.Value;

        first.Equals(second).ShouldBeTrue();
    }

    [Fact]
    public void EqualsReturnsTrueForAnyUnitInstances()
    {
        var first = Unit.Value;
        var second = default(Unit);

        first.Equals(second).ShouldBeTrue();
        (first == second).ShouldBeTrue();
        (first != second).ShouldBeFalse();
        first.Equals((object)second).ShouldBeTrue();
    }

    [Fact]
    public void EqualsReturnsFalseForNonUnitObject()
    {
        Unit.Value.Equals("not a unit").ShouldBeFalse();
    }

    [Fact]
    public void GetHashCodeAlwaysReturnsZero()
    {
        Unit.Value.GetHashCode().ShouldBe(0);
        default(Unit).GetHashCode().ShouldBe(0);
    }

    [Fact]
    public void ToStringReturnsEmptyTupleRepresentation()
    {
        Unit.Value.ToString().ShouldBe("()");
    }

    [Fact]
    public void CompareToAlwaysReturnsZero()
    {
        Unit.Value.CompareTo(default(Unit)).ShouldBe(0);
        ((IComparable)Unit.Value).CompareTo(Unit.Value).ShouldBe(0);
    }

    [Fact]
    public async Task TaskReturnsCompletedTaskWithValue()
    {
        var result = await Unit.Task;

        result.ShouldBe(Unit.Value);
        Unit.Task.IsCompletedSuccessfully.ShouldBeTrue();
    }
}
