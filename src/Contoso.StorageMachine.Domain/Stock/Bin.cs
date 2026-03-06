namespace Contoso.StorageMachine.Stock;

/// <summary>
/// The Storage Machine is specialized in storing plastic bins which can hold a single product. A bin may be empty.
/// </summary>
public sealed record Bin(BinIdentifier Identifier, PartNumber? Content)
{
    /// <summary>Indicates whether the given bin is empty.</summary>
    public bool IsEmpty => Content is null;

    /// <summary>Indicates whether the given bin is not empty, i.e. actually contains a product.</summary>
    public bool IsNotEmpty => !IsEmpty;
}
