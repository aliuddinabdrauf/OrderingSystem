using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Application.Hubs
{
    [Authorize]
    public class OrderHub: Hub
    {
        //no method here, since we will not recieve mesage, only send, and sending message will be from the order service
    }
}
