﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Escyug.LissBinder.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();


            // CUSTOM ROUTES
            //-----------------------------------------
            //config.Routes.MapHttpRoute(
            //   name: "UserRoute",
            //   routeTemplate: "api/User/{login}/{password}",
            //   defaults: new { controller = "User"});


            // DEFAULT ROUTE
            //-----------------------------------------
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
