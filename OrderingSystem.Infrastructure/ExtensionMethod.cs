using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure
{
    public static class  ExtensionMethod
    {
        public static IndexBuilder<T> IsUniqueSoftDelete<T>(this IndexBuilder<T> indexBuilder)
        {
            return indexBuilder.IsUnique().HasFilter("is_deleted = false");
        }
    }
}
