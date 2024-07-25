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
            //unique key with filter, to exclude the data that fullfill the filter, in this case the soft deleted record
            //so if the soft deleted record has unique field with value "a", new record with same value in the same field can still be inserted
            return indexBuilder.IsUnique().HasFilter("is_deleted = false");
        }
    }
}
