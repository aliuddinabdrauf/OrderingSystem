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
    [ApiController]
    public class MenuController(IMenuService menuService, UserManager<IdentityUser<Guid>> userManager) : ControllerBase
    {
        [Authorize(Roles = "ADMIN")]
        [HttpPut]
        [ProducesResponseType(typeof(MenuDto), 200)]
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
        public async Task<IActionResult> GetAllMenus()
        {
            var result = await menuService.GetAllMenus();
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(type: typeof(MenuDto), 200)]
        [Route("{menuId}")]
        public async Task<IActionResult> GetMenuById(Guid menuId)
        {
            var result = await menuService.GetMenuById(menuId);
            return Ok(result);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ProducesResponseType(type: typeof(MenuDto), 200)]
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
        [Route("{menuId}")]
        public async Task<IActionResult> DeleteMenu(Guid menuId)
        {
            Guid userId = new Guid(userManager.GetUserId(User));
            await menuService.DeleteMenu(menuId, userId);
            return NoContent();
        }
    }
}
