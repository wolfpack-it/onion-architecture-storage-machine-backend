namespace Contoso.StorageMachine.Stock;

/// <summary>Defines data access operations for stock functionality.</summary>
public interface IStockRepository
{
    /// <summary>Retrieve all bins currently stored in the Storage Machine.</summary>
    List<Bin> RetrieveAllBins();
}
