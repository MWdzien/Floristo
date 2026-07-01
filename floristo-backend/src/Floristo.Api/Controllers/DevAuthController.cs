using System.Net.Http.Headers;
using System.Net.Http.Json;
using Floristo.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Floristo.Api.Controllers;

[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = false)]
[Route("dev/auth")]
public class DevAuthController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly SupabaseAuthOptions _supabaseOptions;
    private readonly IWebHostEnvironment _environment;

    public DevAuthController(
        IHttpClientFactory httpClientFactory,
        SupabaseAuthOptions supabaseOptions,
        IWebHostEnvironment environment)
    {
        _httpClientFactory = httpClientFactory;
        _supabaseOptions = supabaseOptions;
        _environment = environment;
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetToken([FromBody] DevTokenRequest request)
    {
        if (!_environment.IsDevelopment())
            return NotFound();

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("apikey", _supabaseOptions.PublishableKey);

        var response = await client.PostAsJsonAsync(
            $"{_supabaseOptions.ProjectUrl}/auth/v1/token?grant_type=password",
            new { email = request.Email, password = request.Password });

        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, body);

        return Content(body, "application/json");
    }
}

public record DevTokenRequest(string Email, string Password);
