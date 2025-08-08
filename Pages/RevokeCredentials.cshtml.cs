using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Affinidi_Login_Demo_App.Pages
{
    public class RevokeCredentialsModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Issuance ID")]
        [Required(ErrorMessage = "Issuance ID is required.")]
        public string? IssuanceId { get; set; }

        [BindProperty]
        [Display(Name = "Revocation Reason")]
        [Required(ErrorMessage = "Revocation reason is required.")]
        public string? RevocationReason { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Add logic to revoke the credential using the IssuanceId and RevocationReason
            TempData["SuccessMessage"] = $"Credential with Issuance ID '{IssuanceId}' has been successfully revoked.";

            return RedirectToPage();
        }
    }
}