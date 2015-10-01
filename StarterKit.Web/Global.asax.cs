namespace StarterKit.Web
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using MassTransit;


    public class MvcApplication :
        HttpApplication
    {
        protected void Application_Start()
        {
            // We still keep this around because this sample uses MVC and it relies on System.Web.dll
            // see for more info: http://stackoverflow.com/questions/25032284/migrate-global-asax-to-startup-cs
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}