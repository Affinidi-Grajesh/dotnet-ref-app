using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Affinidi_Login_Demo_App.Util;

namespace Affinidi_Login_Demo_App.Pages
{
    [IgnoreAntiforgeryToken]
    public class CredentialIssuanceModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostIssuePersonalInfo()
        {
            // Use a dynamic object to build the credential data
            dynamic credentialData = new
            {
                name = new
                {
                    givenName = "Grajesh",
                    familyName = "Chandra",
                    nickname = "Grajesh Testing"
                },
                birthdate = "01-01-1990",
                birthCountry = "India",
                citizenship = "Indian",
                phoneNumber = "7666009585",
                nationalIdentification = new
                {
                    idNumber1 = "pan",
                    idType1 = "askjd13212432d"
                },
                email = "grajesh.c@affinidi.com",
                gender = "male",
                maritalStatus = "married",
                verificationStatus = "Completed",
                verificationEvidence = new
                {
                    evidenceName1 = "letter",
                    evidenceURL1 = "http://localhost"
                },
                verificationRemarks = "Done"
            };

            var issuanceInput = new StartIssuanceInput
            {
                claimMode = ClaimModeEnum.TX_CODE,
                data = new List<CredentialData>
                {
                    new CredentialData
                    {
                        credentialTypeId = Environment.GetEnvironmentVariable("PUBLIC_CREDENTIAL_TYPE_ID") ?? "AvvanzPersonalInformationVerification",
                        credentialData = credentialData,
                        metaData = new
                        {
                            expirationDate = "2027-09-01T00:00:00.000Z"
                        }
                    }
                }
            };

            var credentialsClient = new CredentialsClient();
            Console.WriteLine($"Issuance Input: {JsonConvert.SerializeObject(issuanceInput, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })}");
            var issuanceResponse = await credentialsClient.IssuanceStart(issuanceInput);
            Console.WriteLine($"Issuance Response: {JsonConvert.SerializeObject(issuanceResponse)}");

            var credentialOfferUri = issuanceResponse?.CredentialOfferUri ?? "";
            var vaultUrl = Environment.GetEnvironmentVariable("PUBLIC_VAULT_URL") ?? "https://vault.affinidi.com";
            var claimUrl = $"{vaultUrl}/claim?credential_offer_uri={Uri.EscapeDataString(credentialOfferUri)}";

            Console.WriteLine($"Claim URL: {claimUrl}");

            TempData["IssuanceMessage"] = "Email Credential issued. Check logs for details.";
            TempData["ClaimUrl"] = claimUrl;
            TempData["CredentialOfferUri"] = credentialOfferUri;
            TempData["IssuanceId"] = issuanceResponse?.IssuanceId;
            TempData["ExpiresIn"] = issuanceResponse?.ExpiresIn.ToString() ?? "0";
            TempData["TxCode"] = issuanceResponse?.TxCode;
            return RedirectToPage();
        }

        public IActionResult OnPostIssueEducation()
        {
            // Similar approach can be used here for dynamic data
            TempData["IssuanceMessage"] = "Education Verification Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }

        public IActionResult OnPostIssueEmployment()
        {
            // Similar approach can be used here for dynamic data
            TempData["IssuanceMessage"] = "Employment Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }

        public IActionResult OnPostIssueRevocable()
        {
            // Similar approach can be used here for dynamic data
            TempData["IssuanceMessage"] = "Revocable Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }

        public IActionResult OnPostIssueBatch()
        {
            // Similar approach can be used here for dynamic data
            TempData["IssuanceMessage"] = "Batch Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }

        public IActionResult OnPostIssueResidence()
        {
            // Similar approach can be used here for dynamic data
            TempData["IssuanceMessage"] = "Proof of Residence Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }

        public IActionResult OnPostIssueCustom()
        {
            // Similar approach can be used here for dynamic data
            TempData["IssuanceMessage"] = "Custom Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }
    }
}