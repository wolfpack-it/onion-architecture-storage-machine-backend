namespace Contoso.StorageMachine.Stock;

/// <summary>
/// For the purposes of basic stock bookkeeping, a product is represented only by its PartNumber and does not have
/// any other properties. This means that individual products do not have an "identity". Indeed, current software of
/// the Storage Machine does not (yet?) support serial numbers for products.
/// </summary>
public sealed record Product(PartNumber PartNumber);

/// <summary>All products in the Storage Machine are counted by piece.</summary>
public static class StockModel
{
    /// <summary>All products in the given bins.</summary>
    public static List<Product> AllProducts(IEnumerable<Bin> bins)
    {
        // Exercise 0: Fill this in to complete this function. Use type inference as a guide.
        // TODO: Exercise 0: what if a bin occurs multiple times in the input?
        throw new NotImplementedException("Exercise 0: Fill this in to complete this function.");
    }

    /// <summary>Total quantity of each of the provided products.</summary>
    public static Dictionary<Product, int> TotalQuantity(IEnumerable<Product> products)
        // Exercise 0: Fill this in to complete this function. Use type inference as a guide.
        => throw new NotImplementedException("Exercise 0: Fill this in to complete this function.");
}
