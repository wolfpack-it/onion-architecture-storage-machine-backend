using Microsoft.AspNetCore.Builder;

namespace Contoso.StorageMachine.Stock;

// DTOs for JSON serialization of stock types.

/// <summary>JSON representation of a bin.</summary>
public sealed record BinDto(string BinIdentifier, string? Content)
{
    public static BinDto FromBin(Bin bin) => new(bin.Identifier.Value, bin.Content?.Value);
}

/// <summary>JSON representation of a product.</summary>
public sealed record ProductDto(string PartNumber)
{
    // Exercise 0: choose your own serialized representation of a Product and implement it here.
    public static ProductDto FromProduct(Product product)
        => throw new NotImplementedException("Exercise 0: choose your own serialized representation of a Product and implement it here.");
}

/// <summary>JSON representation of an entry in the products overview.</summary>
public sealed record ProductQuantityDto(ProductDto Product, int Total)
{
    public static ProductQuantityDto From(Product product, int quantity)
        => new(ProductDto.FromProduct(product), quantity);
}

/// <summary>This class exposes use-cases of the Stock component as an HTTP Web service.</summary>
public static class StockEndpoints
{
    public static void Map(WebApplication app)
    {
        // An overview of all bins currently stored in the Storage Machine.
        app.MapGet("/bins", (IStockRepository Repository) =>
            StockService.BinOverview(Repository).Select(BinDto.FromBin));

        // An overview of actual stock currently stored in the Storage Machine. Actual stock is defined as all non-empty bins.
        app.MapGet("/stock", (IStockRepository Repository) =>
            StockService.StockOverview(Repository).Select(BinDto.FromBin));

        // An overview of all products stored in the Storage Machine, regardless what bins contain them.
        app.MapGet("/stock/products", (IStockRepository Repository) =>
        {
            // Exercise 0: fill this in to complete this HTTP handler.
            throw new NotImplementedException("Exercise 0: fill this in to complete this HTTP handler.");
        });
    }
}
