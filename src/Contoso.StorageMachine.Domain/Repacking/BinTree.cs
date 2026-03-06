namespace Contoso.StorageMachine.Repacking;

/// <summary>Multiple bins can be nested in each other, thus forming a "tree" of bins.</summary>
public abstract record BinTree
{
    /// <summary>A bin can contain zero or more other (nested) bins and products.</summary>
    public sealed record BinNode(BinIdentifier Id, IReadOnlyList<BinTree> Children) : BinTree;

    /// <summary>A product is represented by its part number.</summary>
    public sealed record BinLeaf(PartNumber PartNumber) : BinTree;

    /// <summary>Determines how many products are contained in all bins of the given bin tree.</summary>
    public static int ProductCount(BinTree binTree) => binTree switch
    {
        BinNode(_, var children) => children.Sum(ProductCount),
        BinLeaf => 1,
        _ => throw new InvalidOperationException($"Unknown BinTree type: {binTree.GetType()}")
    };
}
