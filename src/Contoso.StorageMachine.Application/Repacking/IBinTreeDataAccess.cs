namespace Contoso.StorageMachine.Repacking;

/// <summary>Defines data access operations for repacking functionality.</summary>
public interface IBinTreeDataAccess
{
    /// <summary>
    /// Retrieve the bin tree for the given outer bin (identifier). Result is null when the bin with the given
    /// identifier does not exist.
    /// </summary>
    BinTree? RetrieveBinTree(BinIdentifier binIdentifier);
}
