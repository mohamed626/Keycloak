using Keycloak.Net.Models.Users;
using Keycloak.Services.Keycloak;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Keycloak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IKeycloakServices _keycloakServices;

        public UsersController(IKeycloakServices keycloakServices)
        {
            _keycloakServices = keycloakServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int length = 10, [FromQuery] int start = 0, [FromQuery] string searchValue = "", [FromQuery] string sortColumn = "UserName", [FromQuery] string sortColumnDirection = "asc")
        {

            var users = await _keycloakServices.GetUsersAsync();

            // Apply filtering
            if (!string.IsNullOrEmpty(searchValue))
            {
                users = users.Where(m =>
                    (m.FirstName?.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (m.LastName?.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (m.UserName?.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (m.Email?.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ?? false)
                );
            }

            // Sorting dynamically
            //if (!string.IsNullOrEmpty(sortColumn) && (sortColumnDirection == "asc" || sortColumnDirection == "desc"))
            //{
            //    users = users.AsQueryable().OrderBy($"{sortColumn} {sortColumnDirection}");
            //}

            // Pagination
            var recordsTotal = users.Count();
            var data = users.Skip(start).Take(length).ToList();

            return Ok(new { recordsFiltered = recordsTotal, recordsTotal, data });

        }


        [HttpPost("AddEditUser")]
        public async Task<IActionResult> AddEditUser(DTOs.User user)
        {
            return Ok(await _keycloakServices.AddEditUser(user));
        }
    }
}
