using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure.Databases.OrderingSystem
{
    public class TblMenuGroup: TblBaseSoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<TblMenu> Menus { get; set; }
    }
}
