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
        Task<List<TblMenu>> GetAllMenu(bool? isActive);
        Task<TblMenu> GetMenuById(Guid menuId);
    }

    public class MenuRepository(OrderingSystemDbContext context) : IMenuRepository
    {
        public async Task<TblMenu> GetMenuById(Guid menuId)
        {
            var result = await context.TblMenu.Include(o => o.MenuGroup)
                 .SingleOrDefaultAsync(o => o.Id == menuId) ?? throw new RecordNotFoundException();
            return result;
        }
        public async Task<List<TblMenu>> GetAllMenu(bool? isActive)
        {
            var query = context.TblMenu.Include(o => o.MenuGroup).AsQueryable();
            if (isActive.HasValue)
               query = query.Where(o => o.IsActive == isActive.Value);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
