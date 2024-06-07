using Mapster;
using OrderingSystem.Application.Repositories;
using OrderingSystem.Infrastructure;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using OrderingSystem.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderingSystem.Application.Services
{
    public interface IMenuService
    {
        Task<MenuDto> AddMenu(AddMenuDto menuDto, Guid userId);
        Task<MenuGroupDto> AddMenuGroup(AddMenuGroupDto addMenuGroup, Guid userId);
        Task DeleteMenu(Guid menuId, Guid userId);
        Task DeleteMenuGroup(Guid groupId, Guid userId);
        Task<List<MenuGroupDto>> GetAllMenuGroup();
        Task<List<MenuDto>> GetAllMenus(bool? isActive);
        Task<MenuDto> GetMenuById(Guid menuId);
        Task<MenuGroupDto> GetMenuGroupById(Guid groupId);
        Task<List<MenuDto>> GetMenusByGroup(Guid groupId, bool? isActive);
        Task<MenuByTypeDto> GetMenusGroupedByTypes(bool? isActive);
        Task<MenuDto> UpdateMenu(UpdateMenuDto menuDto, Guid menuId, Guid userId);
        Task<MenuGroupDto> UpdateMenuGroup(UpdateMenuGroupDto menuGroupDto, Guid groupId, Guid userId);
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
            await baseRepository.DeleteDataById<TblMenu>(menuId);
            await baseRepository.SaveChanges(userId);
        }

        public async Task<MenuByTypeDto> GetMenusGroupedByTypes(bool? isActive)
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
        public async Task<MenuGroupDto> AddMenuGroup(AddMenuGroupDto addMenuGroup, Guid userId)
        {
            var toSave = addMenuGroup.Adapt<TblMenuGroup>();
            baseRepository.AddData(toSave);
            await baseRepository.SaveChanges(userId);
            return toSave.Adapt<MenuGroupDto>();
        }
        public async Task<List<MenuGroupDto>> GetAllMenuGroup()
        {
            var result = await baseRepository.GetAllData<TblMenuGroup>();
            return result.Adapt<List<MenuGroupDto>>(); 
        }
        public async Task<MenuGroupDto> GetMenuGroupById(Guid groupId)
        {
            var result = await baseRepository.GetDataById<TblMenuGroup>(groupId);
            return result.Adapt<MenuGroupDto>();
        }
        public async Task<MenuGroupDto> UpdateMenuGroup(UpdateMenuGroupDto menuGroupDto, Guid groupId, Guid userId)
        {
            var dbData = baseRepository.GetDataById<TblMenuGroup>(groupId);
            var toUpdate = menuGroupDto.Adapt(dbData);
            baseRepository.UpdateData(toUpdate);
            await baseRepository.SaveChanges(userId);
            return dbData.Adapt<MenuGroupDto>();
        }
        public async Task DeleteMenuGroup(Guid groupId, Guid userId)
        {
            await baseRepository.DeleteDataById<TblMenuGroup>(groupId);
            await baseRepository.SaveChanges(userId);
        }
        public async Task<List<MenuDto>> GetMenusByGroup(Guid groupId, bool? isActive)
        {
            var result = await baseRepository.GetAllDataWithCondition<TblMenu>(o => o.MenuGroupId == groupId && (isActive == null || o.IsActive == isActive));
            return result.Adapt(new List<MenuDto>());
        }
    }
}
