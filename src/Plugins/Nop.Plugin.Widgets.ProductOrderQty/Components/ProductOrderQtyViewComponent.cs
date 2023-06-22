using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ProductOrderQty.Factory;
using Nop.Plugin.Widgets.ProductOrderQty.Models;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.ProductOrderQty.Components
{
    public class ProductOrderQtyViewComponent : NopViewComponent
    {
        private readonly IProductOrderQtyModelFactory _productOrderQtyModelFactory;

        public ProductOrderQtyViewComponent(IProductOrderQtyModelFactory productOrderQtyModelFactory)
        {
            _productOrderQtyModelFactory = productOrderQtyModelFactory;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (widgetZone.Equals(AdminWidgetZones.ProductDetailsBlock))
            {
                if (additionalData != null)
                {
                    var pModel = additionalData as ProductModel;
                    /*var productExistance = _productOrderQtyModelFactory.GetProductOrderQtyModelAsync(pModel.Id).Result;*/

                    //return View("~/Plugins/Widgets.ProductOrderQty/Views/CreateOrUpdate.cshtml", productExistance);
                    return View("~/Plugins/Widgets.ProductOrderQty/Views/CreateOrUpdate.cshtml", new ProductOrderQtyModel());
                }
                else
                    return View("~/Plugins/Widgets.ProductOrderQty/Views/CreateOrUpdate.cshtml", new ProductOrderQtyModel());
            }

            return Content("");
        }
    }
}
