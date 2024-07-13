using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure.Databases.OrderingSystem
{
    public class TblTable: TblBaseSoftDelete
    {
        public int Number {  get; set; }
        public Guid OrderId { get; set; }
        public ICollection<TblOrder> Orders { get; set; } = [];
    }
}
