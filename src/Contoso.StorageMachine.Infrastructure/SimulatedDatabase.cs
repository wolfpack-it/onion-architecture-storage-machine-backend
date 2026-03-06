using Contoso.StorageMachine.Stock;

namespace Contoso.StorageMachine;

/// <summary>Represents bins nested within a parent bin.</summary>
public sealed record NestedBins(BinIdentifier One, IReadOnlyList<BinIdentifier> More);

public enum SimulatedDatabaseError { BinAlreadyStored }

/// <summary>This class simulates the stock database of the Storage Machine using an in-memory data structure.</summary>
public static class SimulatedDatabase
{
    private static BinIdentifier UnsafeMakeBinId(string s)
        => BinIdentifier.Make(s).Match(ok => ok, _ => throw new InvalidOperationException("Invalid bin identifier in seed data"));

    private static PartNumber UnsafeMakePartNum(string s)
        => PartNumber.Make(s).Match(ok => ok, _ => throw new InvalidOperationException("Invalid part number in seed data"));

    /// <summary>Simulates DB tables which store stock data.</summary>
    private static readonly HashSet<BinIdentifier> _bins;
    private static readonly Dictionary<BinIdentifier, PartNumber> _content;
    private static readonly Dictionary<BinIdentifier, NestedBins> _binStructure;

    static SimulatedDatabase()
    {
        var bins = new[]
        {
            UnsafeMakeBinId("B001"), // 0
            UnsafeMakeBinId("B002"), // 1
            UnsafeMakeBinId("B003"), // 2
            UnsafeMakeBinId("B004"), // 3
            UnsafeMakeBinId("B005"), // 4
            UnsafeMakeBinId("B006"), // 5
        };
        var products = new[]
        {
            UnsafeMakePartNum("1000-1000-1000"),
            UnsafeMakePartNum("2000-1000-1000"),
            UnsafeMakePartNum("3000-1000-1000"),
        };

        _bins = new HashSet<BinIdentifier>(bins);
        _content = new Dictionary<BinIdentifier, PartNumber>
        {
            [bins[1]] = products[0],
            [bins[3]] = products[1],
            [bins[4]] = products[2],
        };
        _binStructure = new Dictionary<BinIdentifier, NestedBins>
        {
            [bins[0]] = new NestedBins(bins[1], new[] { bins[2] }),
            [bins[1]] = new NestedBins(bins[3], Array.Empty<BinIdentifier>()),
            [bins[2]] = new NestedBins(bins[4], new[] { bins[5] }),
        };
    }

    // Public API

    /// <summary>Retrieves all bins from the simulated Storage Machine DB. These are all bins currently stored.</summary>
    public static IReadOnlySet<BinIdentifier> RetrieveBins() => _bins;

    /// <summary>
    /// Retrieves stock for all bins currently stored in the Storage Machine. If the dictionary does not contain a key,
    /// then the corresponding bin does not contain a product (or is not stored in the Machine at all).
    /// </summary>
    public static IReadOnlyDictionary<BinIdentifier, PartNumber> RetrieveStock() => _content;

    /// <summary>Stores a bin in the Storage Machine. It is not allowed to store the same bin "twice".</summary>
    public static Result<Unit, SimulatedDatabaseError> StoreBin(Bin bin)
    {
        if (_bins.Contains(bin.Identifier))
            return Result<Unit, SimulatedDatabaseError>.Error(SimulatedDatabaseError.BinAlreadyStored);

        _bins.Add(bin.Identifier);
        if (bin.Content is { } content)
            _content[bin.Identifier] = content;

        return Result<Unit, SimulatedDatabaseError>.Ok(Unit.Value);
    }

    /// <summary>Retrieves the encoding of how all the bins currently stored in Storage Machine DB are nested in each other.</summary>
    public static IReadOnlyDictionary<BinIdentifier, NestedBins> RetrieveBinNesting() => _binStructure;
}
