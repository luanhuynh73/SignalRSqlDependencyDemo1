﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SignalRDbUpdates
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //OK if use widows log in user, but for sql user need to follow this link
        //https://www.codeproject.com/Articles/12862/Minimum-Database-Permissions-Required-for-SqlDepen
        private static string connString =
@"Data Source=HCTOITDEV1\SQLEXPRESS;Database=SqlDependencyTest;Persist Security Info=false;
  Integrated Security=false;User Id=startUser;Password=startUser";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //Start SqlDependency with application initialization
            SqlDependency.Start(connString);
        }

        protected void Application_End()
        {
            //Stop SQL dependency
            SqlDependency.Stop(connString);
        }
    }
}
