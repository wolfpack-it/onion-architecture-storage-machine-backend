using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Contoso.StorageMachine.Repacking;

// DTOs for JSON serialization of BinTree.

/// <summary>JSON representation of a bin tree node or leaf, with a type discriminator.</summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(BinNodeDto), "bin")]
[JsonDerivedType(typeof(BinLeafDto), "product")]
public abstract record BinTreeDto
{
    /// <summary>A bin node containing an identifier and zero or more child nodes.</summary>
    public sealed record BinNodeDto(string Id, IReadOnlyList<BinTreeDto> Children) : BinTreeDto;

    /// <summary>A product leaf identified by its part number.</summary>
    public sealed record BinLeafDto(string PartNumber) : BinTreeDto;

    public static BinTreeDto FromBinTree(BinTree tree) => tree switch
    {
        BinTree.BinNode node => new BinNodeDto(node.Id.Value, node.Children.Select(FromBinTree).ToList()),
        BinTree.BinLeaf leaf => new BinLeafDto(leaf.PartNumber.Value),
        _ => throw new InvalidOperationException($"Unknown BinTree type: {tree.GetType()}")
    };
}

/// <summary>This class exposes use-cases of the Repacking component as an HTTP Web service.</summary>
public static class RepackingEndpoints
{
    public static void Map(WebApplication app)
    {
        // Retrieve a JSON representation of a single bin tree stored in the Storage Machine.
        app.MapGet("/bin/tree/{binIdentifier}", (string binIdentifier, IBinTreeRepository Repository) =>
            BinIdentifier.Make(binIdentifier).Match<IResult>(
                onOk: id => RepackingService.ViewBinTree(Repository, id) is { } tree
                    ? Results.Ok(BinTreeDto.FromBinTree(tree))
                    : Results.NotFound("The given bin is not stored in the machine"),
                onError: _ => Results.BadRequest("Invalid bin identifier")));

        // Count all products contained in all bins of a single bin tree currently stored in the Storage Machine.
        app.MapGet("/bin/tree/{binIdentifier}/products/count", (string binIdentifier, IBinTreeRepository Repository) =>
            BinIdentifier.Make(binIdentifier).Match<IResult>(
                onOk: id => RepackingService.ProductCount(Repository, id) is { } count
                    ? Results.Text($"The bin tree contains {count} products")
                    : Results.NotFound("The given bin is not stored in the machine"),
                onError: _ => Results.BadRequest("Invalid bin identifier")));
    }
}
