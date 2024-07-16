using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Application.Services;
using OrderingSystem.Infrastructure.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using OrderingSystem.Application.Utils;

namespace OrderingSystem.WebApi.Controllers
{
    [Route("api/menu")]
    [ApiController]
    [Produces("application/json")]
    public class MenuController(IMenuService menuService, UserManager<IdentityUser<Guid>> userManager) : ControllerBase
    {
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
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
        public async Task<IActionResult> GetAllMenus(bool? activeOnly)
        {
            var result = await menuService.GetAllMenus(activeOnly);
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(type: typeof(MenuByTypeDto), 200)]
        [Route("all/grupByType")]
        public async Task<IActionResult> GetMenusGroupedByTypes(bool? activeOnly)
        {
            var result = await menuService.GetMenusGroupedByTypes(activeOnly);
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(type: typeof(MenuDto), 200)]
        [ProducesResponseType(type: typeof(ResponseProblemDto), 404, contentType: "application/problem+json")]
        [Route("{menuId}")]
        public async Task<IActionResult> GetMenuById(Guid menuId)
        {
            var result = await menuService.GetMenuById(menuId);
            return Ok(result);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPut]
        [ProducesResponseType(type: typeof(MenuDto), 200)]
        [ProducesResponseType(type: typeof(ResponseProblemDto), 404, contentType: "application/problem+json")]
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
        [ProducesResponseType(type: typeof(ResponseProblemDto), 404, contentType: "application/problem+json")]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Route("{menuId}")]
        public async Task<IActionResult> DeleteMenu(Guid menuId)
        {
            Guid userId = new Guid(userManager.GetUserId(User));
            await menuService.DeleteMenu(menuId, userId);
            return NoContent();
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [Route("{menuId}/upload/image")]
        [ProducesResponseType(typeof(List<AddFileResultDto>), 200)]
        [ProducesResponseType(typeof(List<AddFileResultDto>), 207)]
        [RequestSizeLimit(1000000)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(ResponseProblemDto),400, contentType: "application/problem+json")]
        public async Task<IActionResult> UploadImages(Guid menuId, [FromForm] IFormFileCollection images, [FromForm] List<Guid> existingImageIds)
        {
            var (result, status) = await menuService.UploadMenuImages(images);
            await menuService.AssignImagesToMenu(menuId, result, existingImageIds);
            if (status == HttpMultiStatus.Success)
                return Ok(result);
            else
                return StatusCode(207, result);
        }
    }
    [Route("api/menu/group")]
    [ApiController]
    [Produces("application/json")]
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
        [ProducesResponseType(typeof(ResponseProblemDto), 404, contentType:"application/problem+json")]
        public async Task<IActionResult> GetMenuGroupById(Guid groupId)
        {
            var result = await menuService.GetMenuGroupById(groupId);
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        [Route("add")]
        [EndpointName("Add Menu Group")]
        [ProducesResponseType(typeof(MenuGroupDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddMenuGroup([FromBody] AddMenuGroupDto addMenuGroup)
        {
            Guid userId = new Guid(userManager.GetUserId(User));
            var result = await menuService.AddMenuGroup(addMenuGroup, userId);
            return Ok(result);
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        [Route("{groupId}")]
        [EndpointName("Update Menu Group")]
        [ProducesResponseType(typeof(MenuGroupDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(ResponseProblemDto), 404, contentType: "application/problem+json")]
        public async Task<IActionResult> UpdateMenuGroup(Guid groupId, [FromBody] UpdateMenuGroupDto updateMenuGroup)
        {
            Guid userId = new Guid(userManager.GetUserId(User));
            var result = await menuService.UpdateMenuGroup(updateMenuGroup, groupId, userId);
            return Ok(result);
        }
        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("{groupId}")]
        [EndpointName("Delete Menu Group")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(ResponseProblemDto), 404, contentType: "application/problem+json")]
        public async Task<IActionResult> DeleteMenuGroup(Guid groupId)
        {
            Guid userId = new Guid(userManager.GetUserId(User));
            await menuService.DeleteMenuGroup(groupId, userId);
            return NoContent();
        }
        [HttpGet]
        [Route("{groupId}/menus")]
        [EndpointName("Get Menus In The Group")]
        [ProducesResponseType(typeof(List<MenuDto>), 200)]
        [ProducesResponseType(typeof(ResponseProblemDto), 404, contentType: "application/problem+json")]
        public async Task<IActionResult> GetMenusByMenuGroupId(Guid groupId)
        {
            var result = await menuService.GetMenusByGroup(groupId, false);
            return Ok(result);
        }
    }
}
