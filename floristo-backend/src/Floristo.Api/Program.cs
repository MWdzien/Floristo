using Floristo.Api.OpenApi;
using Floristo.Application;
using Floristo.Infrastructure;
using Floristo.Infrastructure.Auth;
using Floristo.Shared;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddShared();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSupabaseAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseShared();

app.UseAuthentication();
app.UseAuthorization();

app.MapScalarApiReference();
app.MapControllers();

app.Run();