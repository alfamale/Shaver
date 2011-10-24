using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LoggingViewEngine;
using System.Web.Razor;
using System.Web.WebPages;

namespace FlyByNight
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterViewEngines(ViewEngineCollection viewEngines) 
        { 
            viewEngines.Clear();
            viewEngines.Add(new RazorViewEngine());
            viewEngines.Add(new LoggingViewEngine.LoggingViewEngine());
            viewEngines.Add(new PdfViewEngine.PdfViewEngine());
            viewEngines.Add(new MailerViewEngine.MailerViewEngine());
        }   

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RazorCodeLanguage.Languages.Add("cslog", new CSharpRazorCodeLanguage());
            RazorCodeLanguage.Languages.Add("cspdf", new CSharpRazorCodeLanguage());
            RazorCodeLanguage.Languages.Add("csmail", new CSharpRazorCodeLanguage());

            WebPageHttpHandler.RegisterExtension("cslog");
            WebPageHttpHandler.RegisterExtension("cspdf");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterViewEngines(ViewEngines.Engines);
        }
    }
}