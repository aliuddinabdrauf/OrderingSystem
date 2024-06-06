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
        Task<List<MenuDto>> GetAllMenus();
        Task<MenuDto> GetMenuById(Guid menuId);
        Task<MenuDto> UpdateMenu(UpdateMenuDto menuDto, Guid menuId, Guid userId);
    }

    public class MenuService(IBaseRepository baseRepository) : IMenuService
    {
        public async Task<MenuDto> AddMenu(AddMenuDto menuDto, Guid userId)
        {
            var toSave = menuDto.Adapt<TblMenu>();
            baseRepository.AddData(toSave);
            await baseRepository.SaveChanges(userId);
            return toSave.Adapt<MenuDto>();
        }

        public async Task<List<MenuDto>> GetAllMenus()
        {
            var result = await baseRepository.GetAllData<TblMenu>();
            return result.Adapt<List<MenuDto>>();
        }
        public async Task<MenuDto> GetMenuById(Guid menuId)
        {
            var result = await baseRepository.GetDataById<TblMenu>(menuId);
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
    }
}
