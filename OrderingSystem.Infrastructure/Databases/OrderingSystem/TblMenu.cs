using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure.Databases.OrderingSystem
{
    public class TblMenu: TblBaseSoftDelete
    {
        public string Name { get; set; }
        public MenuType MenuType { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
