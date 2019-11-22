﻿using System.Web.Http;

namespace CinemAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ApiWithId",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "Index" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "Index"}
            );
        }
    }
}