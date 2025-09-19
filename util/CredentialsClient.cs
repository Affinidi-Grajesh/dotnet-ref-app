using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
using AffinidiTdk.AuthProvider;
using AffinidiTdk.CredentialIssuanceClient.Api;
using AffinidiTdk.CredentialIssuanceClient.Client;
using AffinidiTdk.CredentialIssuanceClient.Model;

namespace Affinidi_Login_Demo_App.Util
{
    public class CredentialsClient
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private readonly string projectId;
        private readonly string vaultUrl;

        public CredentialsClient(string projectId, string vaultUrl)
        {
            this.projectId = projectId;
            this.vaultUrl = vaultUrl;
        }

        public async Task<object> StartIssuanceAsync(List<object> data, string claimMode)
        {
            try
            {
                Console.WriteLine("[StartIssuanceAsync] Starting issuance process...");
                Console.WriteLine($"[StartIssuanceAsync] projectId: {projectId}");
                Console.WriteLine($"[StartIssuanceAsync] vaultUrl: {vaultUrl}");
                Console.WriteLine($"[StartIssuanceAsync] claimMode: {claimMode}");
                Console.WriteLine($"[StartIssuanceAsync] data: {JsonConvert.SerializeObject(data)}");

                // Fetch the project scoped token asynchronously
                var projectScopedToken = await AuthProviderClient.Instance.GetProjectScopedToken();
                Console.WriteLine($"[StartIssuanceAsync] projectScopedToken: {(string.IsNullOrEmpty(projectScopedToken) ? "EMPTY" : "REDACTED")}");

                // Configure the API client
                var configuration = new Configuration();
                configuration.AddApiKey("authorization", projectScopedToken);

                HttpClient httpClient = new HttpClient();
                HttpClientHandler httpClientHandler = new HttpClientHandler();
                var apiInstance = new IssuanceApi(httpClient, configuration, httpClientHandler);
                Console.WriteLine("[StartIssuanceAsync] IssuanceApi instance created.");

                var requestJson = new
                {
                    data = data,
                    claimMode = claimMode
                };

                Console.WriteLine($"[StartIssuanceAsync] requestJson: {JsonConvert.SerializeObject(requestJson)}");

                // Map requestJson to StartIssuanceInput
                var startIssuanceInput = JsonConvert.DeserializeObject<StartIssuanceInput>(
                    JsonConvert.SerializeObject(requestJson)
                );

                Console.WriteLine($"[StartIssuanceAsync] startIssuanceInput: {JsonConvert.SerializeObject(startIssuanceInput)}");

                if (startIssuanceInput == null)
                {
                    Console.WriteLine("[StartIssuanceAsync] Error: startIssuanceInput is null.");
                    return new { success = false, error = "Failed to map requestJson to StartIssuanceInput." };
                }

                var apiResponse = await apiInstance.StartIssuanceAsync(projectId, startIssuanceInput);

                Console.WriteLine($"[StartIssuanceAsync] apiResponse: {JsonConvert.SerializeObject(apiResponse)}");

                var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(
                    JsonConvert.SerializeObject(apiResponse)
                );

                // Add vaultLink if needed
                if (response != null && response.ContainsKey("credentialOfferUri"))
                {
                    response["vaultLink"] = $"{vaultUrl}/claim?credential_offer_uri={response["credentialOfferUri"]}";
                    Console.WriteLine($"[StartIssuanceAsync] vaultLink: {response["vaultLink"]}");
                }

                Console.WriteLine("[StartIssuanceAsync] Issuance process completed.");
                return response ?? new Dictionary<string, object>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StartIssuanceAsync] Exception: {ex}");
                return new { success = false, error = ex.Message };
            }
        }
    }
}