using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure.Dtos
{
    public record AddFileDto(string Name, string Extension, byte[] Data)
    {
        public long FileSize = Data.LongLength;
    }
    public record AddFileResultDto(Guid? Id, string Name, string Extension, bool IsSuccess = true);
}
