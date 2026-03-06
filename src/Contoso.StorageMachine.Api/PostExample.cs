using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Contoso.StorageMachine;

/// <summary>
/// Not related to actual functionality of this back-end. Provides an example of some of the ASP.NET Core Minimal API functions.
/// </summary>
public static class PostExample
{
    /// <summary>
    /// An example of how to:
    /// - read JSON from the body of an HTTP request
    /// - return two different "negative" responses, based on the deserialized value
    /// - return a "positive" response
    /// </summary>
    public static void Map(WebApplication app)
    {
        app.MapPost("/number", async (HttpRequest request) =>
        {
            // Decode an integer number from JSON
            int? inputNumber;
            try { inputNumber = await request.ReadFromJsonAsync<int?>(); }
            catch { inputNumber = null; }

            if (inputNumber is null)
                return Results.BadRequest("POST body expected to consist of a single number");
            if (inputNumber.Value % 2 == 0)
                return Results.Text("I don't want an even number", statusCode: 406);
            return Results.Text($"That's a nice odd number {inputNumber.Value}");
        });
    }
}
