using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using AffinidiTdk.CredentialVerificationClient.Api;
using AffinidiTdk.CredentialVerificationClient.Client;
using AffinidiTdk.CredentialVerificationClient.Model;


namespace Affinidi_Login_Demo_App.Util
{

    public class VerifierClient
    {

        public async Task<VerifyCredentialOutput?> VerifyCredentialsAsync(VerifyCredentialInput input)
        {

            Console.WriteLine("[StartIssuanceAsync] Starting Verification process...");
            Console.WriteLine($"[StartIssuanceAsync] Input: {JsonSerializer.Serialize(input)}");

            // Fetch the project scoped token asynchronously
            var projectScopedToken = await AuthProviderClient.Instance.GetProjectScopedToken();
            Console.WriteLine($"[StartIssuanceAsync] projectScopedToken: {(string.IsNullOrEmpty(projectScopedToken) ? "EMPTY" : "REDACTED")}");

            // Configure the API client
            var configuration = new Configuration();
            configuration.AddApiKey("authorization", projectScopedToken);

            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DefaultApi(httpClient, configuration, httpClientHandler);
            Console.WriteLine("[StartVerificationAsync] DefaultApi instance created.");
            {
                // Verifying VC
                VerifyCredentialOutput result = await Task.Run(() => apiInstance.VerifyCredentials(input));
                Console.WriteLine($"[VerifyCredentialsAsync] Verification result: {JsonSerializer.Serialize(result)}");

                return result;
            }

        }

        public async Task<VerifyPresentationOutput?> VerifyPresentationAsync(VerifyPresentationInput input)
        {
            // Fetch the project scoped token asynchronously
            var projectScopedToken = await AuthProviderClient.Instance.GetProjectScopedToken();
            Console.WriteLine($"[StartIssuanceAsync] projectScopedToken: {(string.IsNullOrEmpty(projectScopedToken) ? "EMPTY" : "REDACTED")}");

            // Configure the API client
            var configuration = new Configuration();
            configuration.AddApiKey("authorization", projectScopedToken);

            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new DefaultApi(httpClient, configuration, httpClientHandler);
            Console.WriteLine("[StartIssuanceAsync] IssuanceApi instance created.");
            {
                // Verifying VP
                VerifyPresentationOutput result = await Task.Run(() => apiInstance.VerifyPresentation(input));
                Console.WriteLine($"[VerifyPresentationAsync] Verification result: {JsonSerializer.Serialize(result)}");

                return result;
            }
        }
    }
}
