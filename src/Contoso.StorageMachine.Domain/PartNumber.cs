using System.Text.RegularExpressions;

namespace Contoso.StorageMachine;

/// <summary>All stock (products, components, etc.) is identified by a part number.</summary>
public sealed class PartNumber : IEquatable<PartNumber>
{
    /// <summary>Part numbers have a specific format.</summary>
    private static readonly Regex ValidPartNumber = new(@"^\d{4}-\d{4}-\d{4}$", RegexOptions.Compiled);

    private PartNumber(string value) => Value = value;

    public string Value { get; }

    /// <summary>Construct a valid part number from a raw string or indicate that the string is not a valid part number.</summary>
    public static Result<PartNumber, string> Make(string rawPartNumber)
        => Validation.Matches(
            ValidPartNumber,
            "Part number must have format dddd-dddd-dddd, where d is a digit.",
            rawPartNumber)
            .Map(s => new PartNumber(s));

    public bool Equals(PartNumber? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PartNumber other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
    public override string ToString() => Value;
}
