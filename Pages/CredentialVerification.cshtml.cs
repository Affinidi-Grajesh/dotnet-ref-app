using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Affinidi_Login_Demo_App.Pages
{
    public class CredentialVerificationModel : PageModel
    {
        [BindProperty]
        public string? CredentialData { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Add logic to verify the CredentialData
            return Page();
        }
    }
}