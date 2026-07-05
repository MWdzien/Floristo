using Floristo.Api.OpenApi;
using Floristo.Application;
using Floristo.Infrastructure;
using Floristo.Infrastructure.Auth;
using Floristo.Shared;
using Floristo.Shared.Options;
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

var corsOptions = builder.Configuration.GetOptions<CorsOptions>(CorsOptions.SectionName);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FloristoWeb", policy =>
    {
        policy.WithOrigins(corsOptions.AllowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseShared();

app.UseCors("FloristoWeb");

app.UseAuthentication();
app.UseAuthorization();

app.MapScalarApiReference();
app.MapControllers();

app.Run();