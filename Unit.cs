namespace Core.Result
{
    /// <summary>
    /// A singleton value type representing a void-like success result.
    /// All <see cref="Unit"/> instances are equal.
    /// </summary>
    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
    {
        private readonly static Unit _value = new Unit();

        /// <summary>
        /// The single shared <see cref="Unit"/> instance.
        /// </summary>
        public static ref readonly Unit Value => ref _value;

        /// <summary>
        /// A pre-completed task containing <see cref="Value"/>.
        /// </summary>
        public static Task<Unit> Task { get; } = System.Threading.Tasks.Task.FromResult(Value);

        /// <inheritdoc />
        public int CompareTo(Unit other) => 0;

        /// <inheritdoc />
        int IComparable.CompareTo(object? obj) => 0;

        /// <inheritdoc />
        public override int GetHashCode() => 0;

        /// <inheritdoc />
        public bool Equals(Unit other) => true;

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is Unit;

        /// <summary>
        /// All <see cref="Unit"/> instances are equal.
        /// </summary>
        public static bool operator ==(Unit first, Unit second) => true;

        /// <summary>
        /// All <see cref="Unit"/> instances are equal.
        /// </summary>
        public static bool operator !=(Unit first, Unit second) => false;

        /// <inheritdoc />
        public override string ToString() => "()";
    }
}
