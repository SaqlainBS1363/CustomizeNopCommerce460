using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Infrastructure;

namespace Nop.Plugin.Widgets.VisitorsCrud.Infrastructure
{
    public partial class MyRouteProvider : BaseRouteProvider, IRouteProvider
    {

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            //visitors
            endpointRouteBuilder.MapControllerRoute(name: "VisitorList",
                pattern: $"/visitors",
                defaults: new { controller = "PublicVisitorShow", action = "VisitorList" });
        }

        public int Priority => 0;

    }
}
