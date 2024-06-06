using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure.Databases.OrderingSystem
{
    public class TblOrder : TblBaseSoftDelete
    {
        public int Total { get; set; }
        public string Instruction { get; set; }
        public Guid MenuId { get; set; }
        public TblMenu Menu { get; set; }
        public Guid TableId { get; set; }
        public TblTable Table { get; set; }
        public TblOrderToReciept OrderToReciept { get; set; }
    }
}
