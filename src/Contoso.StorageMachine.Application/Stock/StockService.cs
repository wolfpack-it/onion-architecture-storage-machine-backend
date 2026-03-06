namespace Contoso.StorageMachine.Stock;

/// <summary>An overview of all products in stock: all unique products and their total quantity.</summary>
public sealed record ProductsOverview(IReadOnlyList<(Product Product, int Quantity)> Items);

/// <summary>Provides functionality (use-cases) related to stock (mostly bins) stored in the Storage Machine.</summary>
public static class StockService
{
    /// <summary>An overview of all bins currently stored in the Storage Machine.</summary>
    public static List<Bin> BinOverview(IStockDataAccess dataAccess)
        // Trivially
        => dataAccess.RetrieveAllBins();

    /// <summary>An overview of actual stock currently stored in the Storage Machine. Actual stock is defined as all non-empty bins.</summary>
    public static List<Bin> StockOverview(IStockDataAccess dataAccess)
    {
        // Perform I/O
        var allBins = dataAccess.RetrieveAllBins();
        // Use the model which provides the definition of a bin being (non-)empty
        return allBins.Where(b => b.IsNotEmpty).ToList();
    }

    /// <summary>An overview of all products stored in the Storage Machine, regardless what bins contain them.</summary>
    // Exercise 0: What parameters are needed here?
    public static ProductsOverview ProductsInStock(IStockDataAccess dataAccess)
    {
        // Use the model
        // Exercise 0: Fill this in — call StockModel.AllProducts with the right argument.
        // Exercise 0: Complete this implementation.
        throw new NotImplementedException("Exercise 0: Complete this implementation.");
    }
}
