namespace Contoso.StorageMachine.Repacking;

/// <summary>Provides functionality (use-cases) for nesting of bins and repacking of products within bins.</summary>
public static class RepackingService
{
    /// <summary>
    /// A trivial use-case for retrieving a tree of bins based on the identifier of the outer bin.
    /// Result is null if the outer bin does not exist.
    /// </summary>
    public static BinTree? ViewBinTree(IBinTreeDataAccess dataAccess, BinIdentifier bin)
        => dataAccess.RetrieveBinTree(bin);

    /// <summary>
    /// Count all products contained in all bins of the identified bin tree.
    /// Result is null when there is no bin tree for the provided identifier.
    /// </summary>
    public static int? ProductCount(IBinTreeDataAccess dataAccess, BinIdentifier bin)
        => dataAccess.RetrieveBinTree(bin) is { } binTree
            ? BinTree.ProductCount(binTree)
            : null;
}
