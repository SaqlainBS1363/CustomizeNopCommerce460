using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ProductOrderQty.Factory;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.ProductOrderQty.Controllers
{
    public class PublicVisitorShowController : BasePluginController
    {
        private readonly IProductOrderQtyModelFactory _visitorModelFactory;

        /*public PublicVisitorShowController(IProductOrderQtyModelFactory visitorModelFactory)
        {
            _visitorModelFactory = visitorModelFactory;
        }
        public async Task<IActionResult> VisitorList()
        {
            var publicVisitors = _visitorModelFactory.PreparePublicVisitorModelListAsync().Result;

            return View("~/Plugins/Widgets.VisitorsCrud/Views/VisitorList.cshtml", publicVisitors);
        }*/
    }
}
