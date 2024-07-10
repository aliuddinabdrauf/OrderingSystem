using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderingSystem.Infrastructure;
using OrderingSystem.Infrastructure.Databases.OrderingSystem;
using OrderingSystem.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Application.Repositories
{
    public interface IMenuRepository
    {
        Task<List<MenuDto>> GetAllMenu(bool? activeOnly);
        Task<MenuDto> GetMenuById(Guid menuId);
    }

    public class MenuRepository(OrderingSystemDbContext context) : IMenuRepository
    {
        public async Task<MenuDto> GetMenuById(Guid menuId)
        {
            var result = await context.TblMenu.Include(o => o.MenuGroup).Include(o => o.MenuImages)
                .ProjectToType<MenuDto>()
                 .SingleOrDefaultAsync(o => o.Id == menuId) ?? throw new RecordNotFoundException();
            return result;
        }
        public async Task<List<MenuDto>> GetAllMenu(bool? activeOnly)
        {
            var query = context.TblMenu.Include(o => o.MenuGroup).Include(o => o.MenuImages).AsQueryable();
            if (activeOnly.HasValue)
               query = query.Where(o => o.MenuStatus != MenuStatus.NotActive);
            var result = await query.ProjectToType<MenuDto>().ToListAsync();
            return result;
        }
    }
}
