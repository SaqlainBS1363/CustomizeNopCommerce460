using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.VisitorsCrud.Factory;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.VisitorsCrud.Controllers
{
    public class PublicVisitorShowController : BasePluginController
    {
        private readonly IVisitorModelFactory _visitorModelFactory;

        public PublicVisitorShowController(IVisitorModelFactory visitorModelFactory)
        {
            _visitorModelFactory = visitorModelFactory;
        }
        public async Task<IActionResult> VisitorList()
        {
            var publicVisitors = _visitorModelFactory.PreparePublicVisitorModelListAsync().Result;

            return View("~/Plugins/Widgets.VisitorsCrud/Views/VisitorList.cshtml", publicVisitors);
        }
    }
}
