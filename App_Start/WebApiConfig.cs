using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Taller6.Models;
using System.Web.Http.OData.Builder;

namespace Taller6
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Entity>("Entity");
            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
