using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Affinidi_Login_Demo_App.Pages
{
    public class CredentialIssuanceModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostIssueEmail()
        {
            // This is your backend logic.
            // TODO: Add logic here to call your API for Email Credential issuance.
            // For example, you might use HttpClient to make a POST request.
            // After the API call, you might redirect the user or display a result.

            TempData["IssuanceMessage"] = "Email Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }

        public IActionResult OnPostIssuePhone()
        {
            // This is your backend logic.
            // TODO: Add logic here to call your API for Phone Credential issuance.
            TempData["IssuanceMessage"] = "Phone Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }

        public IActionResult OnPostIssueResidence()
        {
            // This is your backend logic.
            // TODO: Add logic here to call your API for Proof of Residence Credential issuance.
            TempData["IssuanceMessage"] = "Proof of Residence Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }

        public IActionResult OnPostIssueCustom()
        {
            // This is your backend logic.
            // TODO: Add logic here to call your API for Custom Credential issuance.
            TempData["IssuanceMessage"] = "Custom Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }
    }
}