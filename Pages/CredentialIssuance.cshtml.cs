using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Affinidi_Login_Demo_App.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Affinidi_Login_Demo_App.Pages
{
    public class MetaData
    {
        public string expirationDate { get; set; }
    }

    public class CredentialData
    {
        public string credentialTypeId { get; set; }
        public object credentialData { get; set; }

    }

    [IgnoreAntiforgeryToken]
    public class CredentialIssuanceModel : PageModel
    {
        private readonly string projectId;
        private readonly string vaultUrl;

        public CredentialIssuanceModel()
        {
            projectId = Environment.GetEnvironmentVariable("PROJECT_ID") ?? string.Empty;
            vaultUrl = Environment.GetEnvironmentVariable("PUBLIC_VAULT_URL") ?? "https://vault.affinidi.com";
        }

        public void OnGet()
        {
        }

        private async Task<IActionResult> IssueCredential(string credentialTypeId, object credentialData, bool isRevocable, bool isExpiry)
        {
            // Log input parameters
            Console.WriteLine($"[IssueCredential] credentialTypeId: {credentialTypeId}");
            Console.WriteLine($"[IssueCredential] credentialData: {JsonConvert.SerializeObject(credentialData)}");
            Console.WriteLine($"[IssueCredential] isRevocable: {isRevocable}, isExpiry: {isExpiry}");

            // Build the payload dynamically to avoid nulls
            var dataToIssue = new Dictionary<string, object>
            {
                { "credentialTypeId", credentialTypeId },
                { "credentialData", credentialData }
            };

            if (isExpiry)
            {
                dataToIssue["metaData"] = new MetaData
                {
                    expirationDate = "2027-09-01T00:00:00.000Z"
                };
                Console.WriteLine("[IssueCredential] metaData added to payload.");
            }

            if (isRevocable)
            {
                dataToIssue["statusListDetails"] = new List<object>
                {
                    new { purpose = "REVOCABLE", standard = "RevocationList2020" }
                };
                Console.WriteLine("[IssueCredential] statusListDetails added to payload.");
            }

            var dataList = new List<object> { dataToIssue };
            var claimMode = "TX_CODE";

            Console.WriteLine($"[IssueCredential] Final payload: {JsonConvert.SerializeObject(dataList)}");

            var credentialsClient = new CredentialsClient(projectId, vaultUrl);

            object issuanceResponse = null;
            try
            {
                issuanceResponse = await credentialsClient.StartIssuanceAsync(dataList, claimMode);
                Console.WriteLine($"[IssueCredential] Issuance Response: {JsonConvert.SerializeObject(issuanceResponse)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[IssueCredential] Exception during issuance: {ex}");
                TempData["IssuanceMessage"] = $"Error during credential issuance: {ex.Message}";
                return RedirectToPage();
            }

            var responseDict = issuanceResponse as Dictionary<string, object>;
            var credentialOfferUri = responseDict != null && responseDict.ContainsKey("credentialOfferUri")
                ? responseDict["credentialOfferUri"]?.ToString() ?? ""
                : "";
            var claimUrl = $"{vaultUrl}/claim?credential_offer_uri={Uri.EscapeDataString(credentialOfferUri)}";

            TempData["IssuanceMessage"] = $"{credentialTypeId} Credential issued. Check logs for details.";
            TempData["CredentialOfferUri"] = credentialOfferUri;
            TempData["ClaimUrl"] = claimUrl;

            if (responseDict != null)
            {
                TempData["IssuanceId"] = responseDict.ContainsKey("issuanceId") ? responseDict["issuanceId"]?.ToString() : "";
                TempData["ExpiresIn"] = responseDict.ContainsKey("expiresIn") ? responseDict["expiresIn"]?.ToString() : "0";
                TempData["TxCode"] = responseDict.ContainsKey("txCode") ? responseDict["txCode"]?.ToString() : "";
            }
            else
            {
                Console.WriteLine("[IssueCredential] Response is not a dictionary or is null.");
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostIssuePersonalInfo([FromForm] bool revocablePersonalInfo, [FromForm] bool expiryPersonalInfo)
        {
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

            return await IssueCredential(
                Environment.GetEnvironmentVariable("PUBLIC_CREDENTIAL_TYPE_ID") ?? "AvvanzPersonalInformationVerification",
                credentialData, revocablePersonalInfo, expiryPersonalInfo);
        }

        public async Task<IActionResult> OnPostIssueEducation([FromForm] bool revocableEducation, [FromForm] bool expiryEducation)
        {
            dynamic credentialData = new
            {
                candidateDetails = new
                {
                    name = "Grajesh Chandra",
                    phoneNumber = "7666009585",
                    email = "grajesh.c@affinidi.com",
                    gender = "male"
                },
                institutionDetails = new
                {
                    institutionName = "Affinidi",
                    institutionAddress = new
                    {
                        addressLine1 = "Varthur, Gunjur",
                        addressLine2 = "B305, Candeur Landmark, Tower Eiffel",
                        postalCode = "560087",
                        addressRegion = "Karnataka",
                        addressCountry = "India"
                    },
                    institutionContact1 = "+91 1234567890",
                    institutionContact2 = "+91 1234567890",
                    institutionEmail = "test@affinidi.com",
                    institutionWebsiteURL = "affinidi.com"
                },
                educationDetails = new
                {
                    qualification = "Graduation",
                    course = "MBA",
                    graduationDate = "12-08-2013",
                    dateAttendedFrom = "12-08-2011",
                    dateAttendedTo = "12-07-2013",
                    educationRegistrationID = "admins1223454356"
                },
                verificationStatus = "Verified",
                verificationEvidence = new
                {
                    evidenceName1 = "Degree",
                    evidenceURL1 = "http://localhost"
                },
                verificationRemarks = "completed"
            };

            return await IssueCredential(
                Environment.GetEnvironmentVariable("EDUCATION_CREDENTIAL_TYPE_ID") ?? "education_credential_type_id",
                credentialData, revocableEducation, expiryEducation);
        }

        public async Task<IActionResult> OnPostIssueEmployment([FromForm] bool revocableEmployment, [FromForm] bool expiryEmployment)
        {
            dynamic credentialData = new
            {
                candidateDetails = new
                {
                    name = "Grajesh Chandra",
                    phoneNumber = "7666009585",
                    email = "grajesh.c@affinidi.com",
                    gender = "male"
                },
                employerDetails = new
                {
                    companyName = "Affinidi",
                    companyAddress = new
                    {
                        addressLine1 = "Varthur, Gunjur",
                        addressLine2 = "B305, Candeur Landmark, Tower Eiffel",
                        postalCode = "560087",
                        addressRegion = "Karnataka",
                        addressCountry = "India"
                    },
                    hRDetails = new
                    {
                        hRfirstName = "Testing",
                        hRLastName = "HR",
                        hREmail = "hr@affinidi.com",
                        hRDesignation = "Lead HR",
                        hRContactNumber1 = "+911234567789",
                        whenToContact = "9:00-6:00 PM"
                    }
                },
                employmentDetails = new
                {
                    designation = "Testing",
                    employmentStatus = "Fulltime",
                    annualisedSalary = "10000",
                    currency = "INR",
                    tenure = new
                    {
                        fromDate = "05-2022",
                        toDate = "06-2050"
                    },
                    reasonForLeaving = "Resignation",
                    eligibleForRehire = "Yes"
                },
                verificationStatus = "Completed",
                verificationEvidence = new
                {
                    evidenceName1 = "letter",
                    evidenceURL1 = "http://localhost"
                },
                verificationRemarks = "Done"
            };

            return await IssueCredential(
                Environment.GetEnvironmentVariable("EMPLOYMENT_CREDENTIAL_TYPE_ID") ?? "employment_credential_type_id",
                credentialData, revocableEmployment, expiryEmployment);
        }

        public async Task<IActionResult> OnPostIssueResidence([FromForm] bool revocableResidence, [FromForm] bool expiryResidence)
        {
            dynamic credentialData = new
            {
                address = new
                {
                    addressLine1 = "Varthur, Gunjur",
                    addressLine2 = "B305, Candeur Landmark, Tower Eiffel",
                    postalCode = "560087",
                    addressRegion = "Karnataka",
                    addressCountry = "India"
                },
                ownerDetails = new
                {
                    ownerName = "TestOwner",
                    ownerContactDetails1 = "+912325435634"
                },
                neighbourDetails = new
                {
                    neighbourName = "Test Neighbour",
                    neighbourContactDetails1 = "+912325435634"
                },
                stayDetails = new
                {
                    fromDate = "01-01-2000",
                    toDate = "01-01-2020"
                },
                verificationStatus = "Completed",
                verificationEvidence = new
                {
                    evidenceName1 = "Letter",
                    evidenceURL1 = "http://localhost"
                },
                verificationRemarks = "done"
            };

            return await IssueCredential(
                Environment.GetEnvironmentVariable("ADDRESS_CREDENTIAL_TYPE_ID") ?? "address_credential_type_id",
                credentialData, revocableResidence, expiryResidence);
        }

        public async Task<IActionResult> OnPostIssueBatch([FromForm] bool revocableBatch, [FromForm] bool expiryBatch)
        {
            var data = new List<object>();

            // Personal Information Credential
            var personalInfoCredential = new
            {
                credentialTypeId = Environment.GetEnvironmentVariable("PERSONAL_INFORMATION_CREDENTIAL_TYPE_ID") ?? "personal_information_credential_type_id",
                credentialData = new
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
                },
                metaData = expiryBatch ? new MetaData { expirationDate = "2027-09-01T00:00:00.000Z" } : null,
                statusListDetails = revocableBatch ? new List<object> { new { purpose = "REVOCABLE", standard = "RevocationList2020" } } : null
            };
            data.Add(personalInfoCredential);

            // Address Credential
            var addressCredential = new
            {
                credentialTypeId = Environment.GetEnvironmentVariable("ADDRESS_CREDENTIAL_TYPE_ID") ?? "address_credential_type_id",
                credentialData = new
                {
                    address = new
                    {
                        addressLine1 = "Varthur, Gunjur",
                        addressLine2 = "B305, Candeur Landmark, Tower Eiffel",
                        postalCode = "560087",
                        addressRegion = "Karnataka",
                        addressCountry = "India"
                    },
                    ownerDetails = new
                    {
                        ownerName = "TestOwner",
                        ownerContactDetails1 = "+912325435634"
                    },
                    neighbourDetails = new
                    {
                        neighbourName = "Test Neighbour",
                        neighbourContactDetails1 = "+912325435634"
                    },
                    stayDetails = new
                    {
                        fromDate = "01-01-2000",
                        toDate = "01-01-2020"
                    },
                    verificationStatus = "Completed",
                    verificationEvidence = new
                    {
                        evidenceName1 = "Letter",
                        evidenceURL1 = "http://localhost"
                    },
                    verificationRemarks = "done"
                },
                metaData = expiryBatch ? new MetaData { expirationDate = "2027-09-01T00:00:00.000Z" } : null,
                statusListDetails = revocableBatch ? new List<object> { new { purpose = "REVOCABLE", standard = "RevocationList2020" } } : null
            };
            data.Add(addressCredential);

            // Education Credential
            var educationCredential = new
            {
                credentialTypeId = Environment.GetEnvironmentVariable("EDUCATION_CREDENTIAL_TYPE_ID") ?? "education_credential_type_id",
                credentialData = new
                {
                    candidateDetails = new
                    {
                        name = "Grajesh Chandra",
                        phoneNumber = "7666009585",
                        email = "grajesh.c@affinidi.com",
                        gender = "male"
                    },
                    institutionDetails = new
                    {
                        institutionName = "Affinidi",
                        institutionAddress = new
                        {
                            addressLine1 = "Varthur, Gunjur",
                            addressLine2 = "B305, Candeur Landmark, Tower Eiffel",
                            postalCode = "560087",
                            addressRegion = "Karnataka",
                            addressCountry = "India"
                        },
                        institutionContact1 = "+91 1234567890",
                        institutionContact2 = "+91 1234567890",
                        institutionEmail = "test@affinidi.com",
                        institutionWebsiteURL = "affinidi.com"
                    },
                    educationDetails = new
                    {
                        qualification = "Graduation",
                        course = "MBA",
                        graduationDate = "12-08-2013",
                        dateAttendedFrom = "12-08-2011",
                        dateAttendedTo = "12-07-2013",
                        educationRegistrationID = "admins1223454356"
                    },
                    verificationStatus = "Verified",
                    verificationEvidence = new
                    {
                        evidenceName1 = "Degree",
                        evidenceURL1 = "http://localhost"
                    },
                    verificationRemarks = "completed"
                },
                metaData = expiryBatch ? new MetaData { expirationDate = "2027-09-01T00:00:00.000Z" } : null,
                statusListDetails = revocableBatch ? new List<object> { new { purpose = "REVOCABLE", standard = "RevocationList2020" } } : null
            };
            data.Add(educationCredential);

            // Employment Credential
            var employmentCredential = new
            {
                credentialTypeId = Environment.GetEnvironmentVariable("EMPLOYMENT_CREDENTIAL_TYPE_ID") ?? "employment_credential_type_id",
                credentialData = new
                {
                    candidateDetails = new
                    {
                        name = "Grajesh Chandra",
                        phoneNumber = "7666009585",
                        email = "grajesh.c@affinidi.com",
                        gender = "male"
                    },
                    employerDetails = new
                    {
                        companyName = "Affinidi",
                        companyAddress = new
                        {
                            addressLine1 = "Varthur, Gunjur",
                            addressLine2 = "B305, Candeur Landmark, Tower Eiffel",
                            postalCode = "560087",
                            addressRegion = "Karnataka",
                            addressCountry = "India"
                        },
                        hRDetails = new
                        {
                            hRfirstName = "Testing",
                            hRLastName = "HR",
                            hREmail = "hr@affinidi.com",
                            hRDesignation = "Lead HR",
                            hRContactNumber1 = "+911234567789",
                            whenToContact = "9:00-6:00 PM"
                        }
                    },
                    employmentDetails = new
                    {
                        designation = "Testing",
                        employmentStatus = "Fulltime",
                        annualisedSalary = "10000",
                        currency = "INR",
                        tenure = new
                        {
                            fromDate = "05-2022",
                            toDate = "06-2050"
                        },
                        reasonForLeaving = "Resignation",
                        eligibleForRehire = "Yes"
                    },
                    verificationStatus = "Completed",
                    verificationEvidence = new
                    {
                        evidenceName1 = "letter",
                        evidenceURL1 = "http://localhost"
                    },
                    verificationRemarks = "Done"
                },
                metaData = expiryBatch ? new MetaData { expirationDate = "2027-09-01T00:00:00.000Z" } : null,
                statusListDetails = revocableBatch ? new List<object> { new { purpose = "REVOCABLE", standard = "RevocationList2020" } } : null
            };
            data.Add(employmentCredential);

            var claimMode = "TX_CODE";

            var credentialsClient = new CredentialsClient(projectId, vaultUrl);
            var issuanceResponse = await credentialsClient.StartIssuanceAsync(data, claimMode);

            var responseDict = issuanceResponse as Dictionary<string, object>;
            var credentialOfferUri = responseDict != null && responseDict.ContainsKey("credentialOfferUri")
                ? responseDict["credentialOfferUri"]?.ToString() ?? ""
                : "";
            var claimUrl = $"{vaultUrl}/claim?credential_offer_uri={Uri.EscapeDataString(credentialOfferUri)}";

            TempData["IssuanceMessage"] = "Batch Credential issuance process completed. Check logs for details.";
            TempData["ClaimUrl"] = claimUrl;
            TempData["CredentialOfferUri"] = credentialOfferUri;

            if (responseDict != null)
            {
                TempData["IssuanceId"] = responseDict.ContainsKey("issuanceId") ? responseDict["issuanceId"]?.ToString() : "";
                TempData["ExpiresIn"] = responseDict.ContainsKey("expiresIn") ? responseDict["expiresIn"]?.ToString() : "0";
                TempData["TxCode"] = responseDict.ContainsKey("txCode") ? responseDict["txCode"]?.ToString() : "";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostIssueCustom([FromForm] bool revocableCustom, [FromForm] bool expiryCustom)
        {
            TempData["IssuanceMessage"] = "Custom Credential issuance process initiated. Check your backend logs for details.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCheckCredentialStatus()
        {
            TempData["IssuanceMessage"] = "Check Credential Status not implemented.";
            return RedirectToPage();
        }
    }
}
