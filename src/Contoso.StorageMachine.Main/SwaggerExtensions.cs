
namespace Contoso.StorageMachine;

public static class SwaggerExtensions
{
    private const string OpenApiDocumentUrl = "/openapi/v1.json";

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info.Title = "Contoso Storage Machine API";
                document.Info.Version = "v1";
                document.Info.Description = "REST API for the Contoso Storage Machine backend.";
                return Task.CompletedTask;
            });
        });

        return services;
    }

    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        app.MapOpenApi();

        // Classic Swagger UI at /swagger
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(OpenApiDocumentUrl, "Contoso Storage Machine v1");
            options.RoutePrefix = "swagger";
        });

        // In development, serve the OpenAPI document at the root URL for easy access.
        app.MapGet("/", () => Results.Redirect(OpenApiDocumentUrl));

        // Log the UI URLs once the server is listening
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            var logger = app.Services.GetRequiredService<ILoggerFactory>()
                .CreateLogger("SwaggerDocumentation");
            var baseUrl = app.Urls.FirstOrDefault() ?? "https://localhost";
            logger.LogInformation("Swagger UI: {Url}", $"{baseUrl}/swagger/index.html");
        });

        return app;
    }
}
