using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.MsmqIntegration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Workflow.Activities;
using System.Configuration;
using System.Reflection;
using System.Web.Security;
using System.Collections.Concurrent;
using System.Threading;

namespace Crystal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static string applicationVersion = "";

        protected void Application_AuthenticateRequest(object sender, System.EventArgs e)
        {
        }

        void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {

        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignore all ext.axd embedded resource paths
            routes.IgnoreRoute("{extnet-root}/{extnet-file}/ext.axd");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{a}/{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{a}/{b}/{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{a}/{b}/{c}/{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{a}/{b}/{c}/{d}/{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("mini-profiler-resources");
            routes.IgnoreRoute("mini-profiler-resources/{*pathInfo}");
            routes.IgnoreRoute("{myWebServices}.asmx/{*pathInfo}");
            routes.IgnoreRoute("{exclude}/{extnet}/ext.axd");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");


            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

            /**/

            routes.MapRoute(
              "Default2",
              "{controller}.mvc/{action}/{id}",
              new { action = "Index", id = "" }
            );

            routes.MapRoute(
              "Root",
              "",
              new { controller = "Home", action = "Index", id = "" }
            );
        }


        protected void Application_Start()
        {

            RegisterRoutes(RouteTable.Routes);

        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

    }

}