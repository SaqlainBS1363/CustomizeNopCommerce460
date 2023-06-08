using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.VisitorsCrud.Factory;
using Nop.Plugin.Widgets.VisitorsCrud.Models;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.VisitorsCrud.Components
{
    public class WidgetsVisitorsCrudViewComponent : NopViewComponent
    {
        private readonly IVisitorModelFactory _visitorModelFactory;

        public WidgetsVisitorsCrudViewComponent(IVisitorModelFactory visitorModelFactory)
        {
            _visitorModelFactory = visitorModelFactory;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var publicVisitors = _visitorModelFactory.PreparePublicVisitorModelListAsync().Result;

            return View("~/Plugins/Widgets.VisitorsCrud/Views/PublicInfo.cshtml", publicVisitors);
        }
    }
}
