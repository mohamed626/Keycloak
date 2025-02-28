using Keycloak.Net.Models.Users;

namespace Keycloak.Services.Keycloak
{
    public interface IKeycloakServices
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> AddEditUser(DTOs.User user);


    }
}
