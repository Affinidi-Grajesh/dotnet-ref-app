namespace Affinidi_Login_Demo_App.Util
{
    public class AuthProviderClient
    {

        public static string FetchProjectScopedToken()
        {
            var authProvider = new Affinidi.Authprovider.AuthProvider(new Dictionary<string, string>
            {
                ["projectId"] = Environment.GetEnvironmentVariable("PROJECT_ID") ?? string.Empty,
                ["tokenId"] = Environment.GetEnvironmentVariable("TOKEN_ID") ?? string.Empty,
                ["privateKey"] = Environment.GetEnvironmentVariable("PRIVATE_KEY") ?? string.Empty,
                ["passphrase"] = Environment.GetEnvironmentVariable("PASSPHRASE") ?? string.Empty,
            });

            var token = authProvider.FetchProjectScopedToken();
            Console.WriteLine($"Fetched token: {token}");

            return token;
        }
    }
}