
namespace Contoso.StorageMachine;

/// <summary>Every bin stored in the Storage Machine is identified by a unique identifier.</summary>
public sealed class BinIdentifier : IEquatable<BinIdentifier>
{
    private BinIdentifier(string value) => Value = value;

    public string Value { get; }

    /// <summary>Construct a valid bin identifier from a raw string or indicate that the string is not a valid bin identifier.</summary>
    public static Result<BinIdentifier, string> Make(string rawIdentifier)
        => Validation.NonEmpty("Bin identifier may not be empty.", rawIdentifier)
            .Bind(s => Validation.AlphaNumeric("Bin identifier may contain only letters or digits.", s))
            .Map(s => new BinIdentifier(s));

    public bool Equals(BinIdentifier? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is BinIdentifier other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
}
