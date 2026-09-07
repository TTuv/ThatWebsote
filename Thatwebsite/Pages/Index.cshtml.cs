using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Thatwebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public IndexModel(IConfiguration config, ILogger<IndexModel> logger)
        {
            _config = config;
            _logger = logger;
        }

        public bool IsAuthenticated { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            try
            {
                IsAuthenticated = HttpContext.Session.GetString("authenticated") == "true";
            }
            catch
            {
                IsAuthenticated = false;
            }
        }

        public async Task<IActionResult> OnPostValidatePassword(string password)
        {
            var configured = _config["PagePassword"] ?? "__mfavourite";

            if (password == configured)
            {
                HttpContext.Session.SetString("authenticated", "true");
                IsAuthenticated = true;

                try
                {
#if !DEBUG
                    await Notify.Loggin();
                    _logger.LogInformation("Notify.Loggin() completed successfully.");
#endif
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Notify.Loggin() threw an exception.");
                    // optional: expose friendly message in UI for debugging
                    ErrorMessage += " (notification failed)";
                }

                return RedirectToPage();
            }

            ErrorMessage = "Incorrect password";
            IsAuthenticated = false;
            
            return Page();
        }
    }
}