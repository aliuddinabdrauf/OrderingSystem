using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Application.Services;
using OrderingSystem.Infrastructure.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace OrderingSystem.WebApi.Controllers
{
    [Route("api/menu")]
    [EndpointGroupName("Menu")]
    [ApiController]
    public class MenuController(IMenuService menuService, UserManager<IdentityUser<Guid>> userManager) : ControllerBase
    {
        [Authorize(Roles = "ADMIN")]
        [HttpPut]
        [ProducesResponseType(typeof(MenuDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Route("add")]
        public async Task<IActionResult> AddMenu([FromBody] AddMenuDto addMenu)
        {
            Guid userId = new Guid(userManager.GetUserId(User));
            var result = await menuService.AddMenu(addMenu, userId);
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(type: typeof(List<MenuDto>), 200)]
        [Route("all")]
        public async Task<IActionResult> GetAllMenus(bool? isActive)
        {
            var result = await menuService.GetAllMenus(isActive);
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(type: typeof(MenuByTypeDto), 200)]
        [Route("all/grupByType")]
        public async Task<IActionResult> GetMenusGroupedByTypes(bool? isActive)
        {
            var result = await menuService.GetMenusGroupedByTypes(isActive);
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(type: typeof(MenuDto), 200)]
        [ProducesResponseType(type: typeof(ResponseProblemDto), 404)]
        [Route("{menuId}")]
        public async Task<IActionResult> GetMenuById(Guid menuId)
        {
            var result = await menuService.GetMenuById(menuId);
            return Ok(result);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ProducesResponseType(type: typeof(MenuDto), 200)]
        [ProducesResponseType(type: typeof(ResponseProblemDto), 404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Route("{menuId}")]
        public async Task<IActionResult> UpdateMenu([FromBody]UpdateMenuDto menuDto, Guid menuId)
        {
            Guid userId = new Guid(userManager.GetUserId(User));
            var result = await menuService.UpdateMenu(menuDto, menuId, userId);
            return Ok(result);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(type: typeof(ResponseProblemDto), 404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Route("{menuId}")]
        public async Task<IActionResult> DeleteMenu(Guid menuId)
        {
            Guid userId = new Guid(userManager.GetUserId(User));
            await menuService.DeleteMenu(menuId, userId);
            return NoContent();
        }
    }
    [Route("api/menu/group")]
    [ApiController]
    [EndpointGroupName("Menu Group")]
    public class MenuGroupController(IMenuService menuService, UserManager<IdentityUser<Guid>> userManager) : ControllerBase
    {
        [HttpGet]
        [Route("all")]
        [EndpointName("Get All Menu Group")]
        [ProducesResponseType(typeof(List<MenuGroupDto>), 200)]
        public async Task<IActionResult> GetAllMenuGroup()
        {
            var result = await menuService.GetAllMenuGroup();
            return Ok(result);
        }
        [HttpGet]
        [Route("{groupId}")]
        [EndpointName("Get Menu Group By Id")]
        [ProducesResponseType(typeof(MenuGroupDto), 200)]
        [ProducesResponseType(typeof(ResponseProblemDto), 404)]
        public async Task<IActionResult> GetMenuGroupById(Guid groupId)
        {
            var result = await menuService.GetMenuGroupById(groupId);
            return Ok(result);
        }
    }
}
