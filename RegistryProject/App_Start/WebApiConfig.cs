using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RegistryProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            config.Formatters.Insert(0, new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            // Apply this GLOBALLY so that we don 't have to be bothered with it during other JSON operations
            Newtonsoft.Json.JsonConvert.DefaultSettings = () =>
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    ReferenceLoopHandling = Newtonsoft.Json.
                    ReferenceLoopHandling.Ignore
                };

            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
        }
    }
}
