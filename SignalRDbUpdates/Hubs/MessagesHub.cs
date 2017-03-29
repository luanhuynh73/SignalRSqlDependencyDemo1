﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRDbUpdates.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SignalRDbUpdates.Hubs
{
    public class MessagesHub : Hub
    {
       

        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            context.Clients.All.updateMessages2();
        }

        
    }
}