using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using RTMS_API;

namespace RTMS_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<SensorCode>("SensorCodes");
            builder.EntitySet<TempReading>("TempReadings");
            builder.EntitySet<WifiActivity>("WifiActivities");
            config.Routes.MapODataServiceRoute("odata", null, builder.GetEdmModel());
        }
    }
}
