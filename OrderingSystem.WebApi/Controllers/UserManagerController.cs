using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Infrastructure;

namespace OrderingSystem.WebApi.Controllers
{
    [Route("api/usermanager")]
    [ApiController]
    public class UserManagerController(RoleManager<IdentityRole<Guid>> roleManager, UserManager<IdentityUser<Guid>> userManager) : ControllerBase
    {
        [HttpPut]
        [Route("role/add")]
        [ProducesResponseType(typeof(IdentityRole<Guid>), 200)]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var role = new IdentityRole<Guid> { Name = roleName };
            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new NoDataUpdatedException(error.Description);
                }
            }
            return Ok(role);
        }
        [HttpGet]
        [Route("role/getAll")]
        [ProducesResponseType(typeof(List<IdentityRole<Guid>>), 200)]
        public  IActionResult GetAllRole()
        {
           return Ok(roleManager.Roles.ToList());
        }
        [HttpDelete]
        [Route("role/delete/{roleId}")]
        public async Task<IActionResult> DeleteRole(Guid roleId)
        {
          var result =  await roleManager.DeleteAsync(new IdentityRole<Guid> { Id = roleId });
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new NoDataUpdatedException(error.Description);
                }
            }
            return NoContent();
        }
        [HttpPost]
        [Route("role/assign/{userId}")]
        public async Task<IActionResult> AssignRolesToUser(Guid userId, [FromBody] List<string> roleName)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if(user == null)
            {
                throw new RecordNotFoundException("user not exist");
            }
            var roles = await userManager.GetRolesAsync(user);
            if(roles != null &&  roles.Count > 0)
            {
                var removeResult = await userManager.RemoveFromRolesAsync(user, roles);
                if (!removeResult.Succeeded)
                {
                    throw new NoDataUpdatedException("cannot remove existing role");
                }
            }
            var assignResult = await userManager.AddToRolesAsync(user, roleName);
            if (!assignResult.Succeeded)
            {
                throw new NoDataUpdatedException("cannot add roles");
            }
            return NoContent();
        }
    }
}
