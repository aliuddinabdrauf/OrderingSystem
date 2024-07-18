using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure.Databases.OrderingSystem
{
    public class TblOrderToReciept: TblBase
    {
        public Guid OrderId { get; set; }
        public TblOrder Order { get; init; } = null!;
        public Guid RecieptId { get; set; }
        public TblReciept Reciept { get; init; } = null!;
    }
}
