namespace Core.Result
{
    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
    {
        private readonly static Unit _value = new Unit();

        public static ref readonly Unit Value => ref _value;

        public static Task<Unit> Task { get; } = System.Threading.Tasks.Task.FromResult(Value);

        public int CompareTo(Unit other) => 0;

        int IComparable.CompareTo(object? obj) => 0;

        public override int GetHashCode() => 0;

        public bool Equals(Unit other) => true;

        public override bool Equals(object? obj) => obj is Unit;

        public static bool operator ==(Unit first, Unit second) => true;

        public static bool operator !=(Unit first, Unit second) => false;

        public override string ToString() => "()";
    }
}
