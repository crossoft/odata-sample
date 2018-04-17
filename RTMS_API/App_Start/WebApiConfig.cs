using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using RTMS_API;

namespace RTMS_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<SensorCode>("SensorCodes");
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
            //config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            //ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<SensorCode>("SensorCodes");
            //config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
