using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure.Databases.OrderingSystem
{
    public class TblFile: TblBase
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public long FileSize { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<TblMenuImage> MenuImages { get; set; }
    }
}
