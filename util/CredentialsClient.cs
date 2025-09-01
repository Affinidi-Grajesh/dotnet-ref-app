using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Affinidi_Login_Demo_App.Util
{
    public enum ClaimModeEnum { NORMAL, TX_CODE, FIXED_HOLDER }
    // NOTE: The following classes are placeholders for the actual models from Affinidi's .NET SDKs.
    // Please replace them with the actual classes from the SDKs.
    public class StartIssuanceInput
    {

        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
        public ClaimModeEnum claimMode { get; set; }
        // public string? holderDid { get; set; }
        public List<CredentialData> data { get; set; } = new List<CredentialData>();
    }
    public class CredentialData
    {
        public string credentialTypeId { get; set; } = string.Empty;
        public object? credentialData { get; set; }
        public object? metaData { get; set; }

        public List<dynamic>? statusListDetails { get; set; } // Added this property

    }

    public class StartIssuanceResponse
    {
        [JsonPropertyName("credentialOfferUri")]
        public string? CredentialOfferUri { get; set; }

        [JsonPropertyName("issuanceId")]
        public string? IssuanceId { get; set; }

        [JsonPropertyName("expiresIn")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("txCode")]
        public string? TxCode { get; set; }


    }

    public class IssuanceStatusResponse
    {
        [JsonPropertyName("credential")]
        public CredentialData? Credential { get; set; }

        [JsonPropertyName("credentials")]
        public List<CredentialData>? Credentials { get; set; }
    }


    public class ApiResponse<T> { public T? Data { get; set; } }

    public class IssuanceConfiguration { public required string BasePath { get; set; } }

    public class IssuanceApi
    {
        IssuanceConfiguration _config;
        public IssuanceApi(IssuanceConfiguration config)
        {
            _config = config;
        }
        public virtual async Task<ApiResponse<StartIssuanceResponse>> StartIssuanceAsync(string projectId, StartIssuanceInput input)
        {
            var localVarPath = $"cis/v1/{Uri.EscapeDataString(projectId)}/issuance/start";
            var fullUrl = new Uri(new Uri(_config.BasePath), localVarPath).ToString();
            var token = AuthProviderClient.FetchProjectScopedToken();

            // Use System.Text.Json with options to ignore null values
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true // Optional: for readability in console logs
            };

            var jsonPayload = System.Text.Json.JsonSerializer.Serialize(input, options);

            Console.WriteLine($"Issuance API request: {jsonPayload}");
            Console.WriteLine($"Authorization token: {token}");

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Post, fullUrl)
            {
                Content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json")
            };
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var data = System.Text.Json.JsonSerializer.Deserialize<StartIssuanceResponse>(responseBody);
                Console.WriteLine($"Issuance API response: {responseBody}");
                Console.WriteLine($"CredentialOfferUri: {data?.CredentialOfferUri}");
                Console.WriteLine($"IssuanceId: {data?.IssuanceId}");
                Console.WriteLine($"ExpiresIn: {data?.ExpiresIn}");
                Console.WriteLine($"TxCode: {data?.TxCode}");
                return new ApiResponse<StartIssuanceResponse> { Data = data };
            }
            Console.WriteLine($"Issuance API error: {response.StatusCode}");
            return new ApiResponse<StartIssuanceResponse> { Data = null };
        }
        public virtual async Task<ApiResponse<IssuanceStatusResponse>> GetIssuanceStatusAsync(string issuanceId)
        {
            var projectId = Environment.GetEnvironmentVariable("PROJECT_ID") ?? string.Empty;
            var configurationId = Environment.GetEnvironmentVariable("CONFIGURATION_ID") ?? string.Empty;

            var localVarPath = $"cis/v1/{Uri.EscapeDataString(projectId)}/configurations/{Uri.EscapeDataString(configurationId)}/issuances/{Uri.EscapeDataString(issuanceId)}/credentials";
            var fullUrl = new Uri(new Uri(_config.BasePath), localVarPath).ToString();
            var token = AuthProviderClient.FetchProjectScopedToken();

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(fullUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var data = System.Text.Json.JsonSerializer.Deserialize<IssuanceStatusResponse>(responseBody);
                Console.WriteLine($"Issuance Status API response: {responseBody}");
                return new ApiResponse<IssuanceStatusResponse> { Data = data };
            }
            else
            {
                Console.WriteLine($"Issuance Status API error: {response.StatusCode}");
                return new ApiResponse<IssuanceStatusResponse> { Data = null };
            }
        }

    }

    public class CredentialsClient
    {
        private readonly IssuanceApi _issuanceApi;
        private string apiGatewayUrl;
        private string projectId;

        public CredentialsClient()
        {
            apiGatewayUrl = Environment.GetEnvironmentVariable("API_GATEWAY_URL") ?? string.Empty;
            projectId = Environment.GetEnvironmentVariable("PROJECT_ID") ?? string.Empty;
            // Assuming SDK configuration objects
            var issuanceConfig = new IssuanceConfiguration { BasePath = $"{apiGatewayUrl}/cis" };
            Console.WriteLine($"Issuance API Base Path: {issuanceConfig.BasePath}");
            _issuanceApi = new IssuanceApi(issuanceConfig);


        }

        public async Task<StartIssuanceResponse?> IssuanceStart(StartIssuanceInput apiData)
        {
            var response = await _issuanceApi.StartIssuanceAsync(projectId, apiData);
            return response.Data;
        }


        public async Task<IssuanceStatusResponse?> IssuanceStatus(string issuanceId)
        {
            var response = await _issuanceApi.GetIssuanceStatusAsync(issuanceId);
            return response.Data;
        }
    }

}
