namespace Contoso.StorageMachine.Repacking;

/// <summary>Data access operations of the Repacking component implemented using the simulated in-memory DB.</summary>
public sealed class RepackingDataAccess : IBinTreeDataAccess
{
    public BinTree? RetrieveBinTree(BinIdentifier outerBin)
    {
        var bins = SimulatedDatabase.RetrieveBins();
        var binStructure = SimulatedDatabase.RetrieveBinNesting();
        var products = SimulatedDatabase.RetrieveStock();

        if (!bins.Contains(outerBin))
            return null;

        return BuildTree(outerBin);

        // A helper for constructing an actual bin tree from the low-level encoding of bin nesting in the simulated DB.
        BinTree BuildTree(BinIdentifier bin)
        {
            // Locate all inner bins of the outer bin
            var innerBins = binStructure.TryGetValue(bin, out var nested)
                ? new[] { nested.One }.Concat(nested.More)
                : Enumerable.Empty<BinIdentifier>();

            // The outer bin may or may not contain a product
            var productLeaf = products.TryGetValue(bin, out var partNum)
                ? new BinTree.BinLeaf(partNum)
                : (BinTree?)null;

            // Combine the outer bin, its optional product and sub-trees into a tree node
            var children = (productLeaf is not null
                    ? new[] { (BinTree)productLeaf }
                    : Array.Empty<BinTree>())
                .Concat(innerBins.Select(BuildTree))
                .ToList();

            return new BinTree.BinNode(bin, children);
        }
    }
}
