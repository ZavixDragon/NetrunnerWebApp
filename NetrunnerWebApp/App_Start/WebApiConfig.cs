using NetrunnerWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace NetrunnerWebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var bootstrap = new Bootstrap();
            var container = bootstrap.GetContainer(); 
            config.DependencyResolver = new UnityResolver(container);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
