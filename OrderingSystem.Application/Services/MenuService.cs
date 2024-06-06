using Mapster;
using OrderingSystem.Application.Repositories;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using OrderingSystem.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Application.Services
{
    public interface IMenuService
    {
        Task<MenuDto> AddMenu(AddMenuDto menuDto, Guid userId);
        Task DeleteMenu(Guid menuId, Guid userId);
        Task<List<MenuDto>> GetAllMenus(bool? isActive);
        Task<MenuDto> GetMenuById(Guid menuId);
        Task<MenuByTypeDto> GetMenusByTypes(bool? isActive);
        Task<MenuDto> UpdateMenu(UpdateMenuDto menuDto, Guid menuId, Guid userId);
    }

    public class MenuService(IBaseRepository baseRepository, IMenuRepository menuRepository) : IMenuService
    {
        public async Task<MenuDto> AddMenu(AddMenuDto menuDto, Guid userId)
        {
            var toSave = menuDto.Adapt<TblMenu>();
            baseRepository.AddData(toSave);
            await baseRepository.SaveChanges(userId);
            return toSave.Adapt<MenuDto>();
        }

        public async Task<List<MenuDto>> GetAllMenus(bool? isActive)
        {
            var result = await menuRepository.GetAllMenu(isActive);
            return result.Adapt<List<MenuDto>>();
        }
        public async Task<MenuDto> GetMenuById(Guid menuId)
        {
            var result = await menuRepository.GetMenuById(menuId);
            return result.Adapt<MenuDto>();
        }
        public async Task<MenuDto> UpdateMenu(UpdateMenuDto menuDto, Guid menuId, Guid userId)
        {
            var record = await baseRepository.GetDataById<TblMenu>(menuId);
            var toSave = menuDto.Adapt(record);
            baseRepository.UpdateData(toSave);
            await baseRepository.SaveChanges(userId);
            return toSave.Adapt<MenuDto>();
        }
        public async Task DeleteMenu(Guid menuId, Guid userId)
        {
            var toDelete = await baseRepository.GetDataById<TblMenu>(menuId);
            baseRepository.DeleteData(toDelete);
            await baseRepository.SaveChanges(userId);
        }

        public async Task<MenuByTypeDto> GetMenusByTypes(bool? isActive)
        {
            var allMenu = await menuRepository.GetAllMenu(isActive);
            var menuGrouped = allMenu.Adapt<List<MenuDto>>().GroupBy(o => o.MenuType);
            var result = new MenuByTypeDto();
            foreach(var menu in menuGrouped)
            {
                switch (menu.Key)
                {
                    case MenuType.Others:
                        result.Others = [.. menu];
                        break;
                    case MenuType.MainCourse:
                        result.MainCourse = [.. menu];
                        break;
                    case MenuType.Dessert:
                        result.Desert = [.. menu];
                        break;
                    case MenuType.Drinks:
                        result.Drinks = [.. menu];
                        break;
                }
            }
            return result;
        }
    }
}
