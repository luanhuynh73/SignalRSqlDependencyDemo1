﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignalRDbUpdates.Startup))]
namespace SignalRDbUpdates
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //testing stuff
            ConfigureAuth(app);
            app.MapSignalR();   
        }
    }
}
