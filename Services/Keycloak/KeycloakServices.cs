using Keycloak.Net;
using Keycloak.Net.Models.Users;
using System.Text.Json;

namespace Keycloak.Services.Keycloak
{
    public class KeycloakServices:IKeycloakServices
    {
        private readonly string _realm;
        private readonly string _keycloakUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;

        private readonly KeycloakClient _keycloakClient;
        public KeycloakServices(IConfiguration configuration)
        {
            _keycloakUrl = configuration["Keycloak:Internal_Host"];
            _clientId = configuration["Keycloak:ClientId"];
            _clientSecret = configuration["Keycloak:ClientSecret"];

            Func<string> tokenProvider = () => GetAccessTokenAsync().GetAwaiter().GetResult();
            _keycloakClient = new KeycloakClient(_keycloakUrl, tokenProvider);
            _realm = configuration["KeyCloak:Realm"]; ;
        }


        public async Task<string> GetAccessTokenAsync()
        {
            var tokenUrl = $"{_keycloakUrl}/realms/{_realm}/protocol/openid-connect/token";

            var requestData = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret", _clientSecret),
            new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            try
            {
                var response = await new HttpClient().PostAsync(tokenUrl, requestData);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                using var jsonDocument = JsonDocument.Parse(responseBody);

                return jsonDocument.RootElement.GetProperty("access_token").GetString();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed to get token: {ex.Message}");
                throw;
            }
        }
      
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            IEnumerable<User>? users = await _keycloakClient.GetUsersAsync(_realm);
            return users;
        }

        public async Task<User> AddEditUser(DTOs.User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Id))
                {

                    await _keycloakClient.CreateUserAsync(_realm, new User { FirstName=user.FirstName,LastName=user.LastName,Email=user.Email,UserName=user.UserName});
                    return (await _keycloakClient.GetUsersAsync(_realm)).Where(u => u.Email == user.Email).FirstOrDefault();
                }
                else
                {
                    var olduser =await _keycloakClient.GetUserAsync(_realm, user.Id);
                    olduser.FirstName = user.FirstName;
                    olduser.LastName = user.LastName;   
                    olduser.Email = user.Email;
                    olduser.UserName=user.UserName;
                    await _keycloakClient.UpdateUserAsync(_realm, olduser.Id, olduser);
                    return olduser;
                }
            }
            catch
            {
                return new();
            }
           
        }
    }
}
