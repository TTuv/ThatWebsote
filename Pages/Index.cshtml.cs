using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

public class IndexModel : PageModel
{
    private readonly IConfiguration _config;

    public IndexModel(IConfiguration config)
    {
        _config = config;
    }

    public void OnGet()
    {
    }

    public class PasswordRequest
    {
        public string Password { get; set; }
    }

    // NOTE: For production, validate antiforgery tokens instead of ignoring them.
    [IgnoreAntiforgeryToken]
    public IActionResult OnPostValidatePassword([FromBody] PasswordRequest req)
    {
        var configured = _config["PagePassword"];
        if (string.IsNullOrEmpty(configured))
        {
            // fallback test password (change or remove for production)
            configured = "secret";
        }

        bool ok = req != null && req.Password == configured;
        return new JsonResult(new { success = ok });
    }
}