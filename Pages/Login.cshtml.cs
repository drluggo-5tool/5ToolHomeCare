using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FiveToolHomeCare.Pages;

public class LoginModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher<string> _passwordHasher;

    public LoginModel(
        IConfiguration configuration,
        IPasswordHasher<string> passwordHasher)
    {
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }

    [BindProperty]
    public string Username { get; set; } = "";

    [BindProperty]
    public string Password { get; set; } = "";

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var adminUsername = _configuration["Admin:Username"];
        var passwordHash = _configuration["Admin:PasswordHash"];

        if (string.IsNullOrWhiteSpace(adminUsername) ||
            string.IsNullOrWhiteSpace(passwordHash))
        {
            ModelState.AddModelError(
                "",
                "Administrator login is not configured."
            );

            return Page();
        }

        var passwordResult = _passwordHasher.VerifyHashedPassword(
            adminUsername,
            passwordHash,
            Password
        );

        if (Username != adminUsername ||
            passwordResult == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(
                "",
                "Invalid username or password."
            );

            return Page();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, adminUsername),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );

        return RedirectToPage("/Admin");
    }
}