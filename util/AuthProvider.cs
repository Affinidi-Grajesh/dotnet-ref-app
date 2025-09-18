
using AffinidiTdk.AuthProvider;

namespace Affinidi_Login_Demo_App.Util
{
    public class AuthProviderClient
    {

        public static async Task<string> FetchProjectScopedToken()
        {
            var projectId = Environment.GetEnvironmentVariable("PROJECT_ID") ?? string.Empty;
            var tokenId = Environment.GetEnvironmentVariable("TOKEN_ID") ?? string.Empty;
            var privateKey = Environment.GetEnvironmentVariable("PRIVATE_KEY") ?? string.Empty;
            var passphrase = Environment.GetEnvironmentVariable("PASSPHRASE") ?? string.Empty;

            var authProviderParams = new AuthProviderParams
            {
                ProjectId = projectId,
                TokenId = tokenId,
                PrivateKey = privateKey,
                Passphrase = passphrase
            };
            var authProvider = new AuthProvider(authProviderParams);

            var token = await authProvider.FetchProjectScopedTokenAsync();
            Console.WriteLine($"Fetched token: {token}");

            return token;
        }
    }
}