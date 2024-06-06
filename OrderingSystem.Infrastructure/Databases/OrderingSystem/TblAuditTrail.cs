﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Infrastructure.Databases.OrderingSystem
{
    public class TblAuditTrail
    {
        public Guid Id { get; set; }
        public Guid ActorId { get; set; }
        public EntityState Action { get; set; }
        public string TableName { get; set; }
        public Guid TableId { get; set; }
        public DateTimeOffset ActionTimestamp { get; set; }
        public string Data { get; set; }
    }
}
