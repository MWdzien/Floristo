using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Floristo.Api.OpenApi;

internal sealed class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Paste the access_token returned from POST /dev/auth/token (without the 'Bearer ' prefix)."
        };

        var requirement = new OpenApiSecurityRequirement
        {
            [new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
            }] = []
        };

        foreach (var operation in document.Paths.Values.SelectMany(p => p.Operations.Values))
        {
            operation.Security.Add(requirement);
        }

        return Task.CompletedTask;
    }
}
