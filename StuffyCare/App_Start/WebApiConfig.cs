﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StuffyCare
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.EnableCors();
            config.MapHttpAttributeRoutes();
            //config.Filters.Add(new StuffyCare.Filters.ExceptionFilter());
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
