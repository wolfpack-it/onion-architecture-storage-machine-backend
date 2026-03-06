namespace Contoso.StorageMachine.Stock;

/// <summary>Data access operations of the Stock component implemented using the simulated in-memory DB.</summary>
public sealed class StockRepository : IStockRepository
{
    public List<Bin> RetrieveAllBins()
    {
        var stock = SimulatedDatabase.RetrieveStock();
        return SimulatedDatabase.RetrieveBins()
            .Select(binId => new Bin(
                binId,
                stock.TryGetValue(binId, out var partNum) ? partNum : null))
            .ToList();
    }
}
