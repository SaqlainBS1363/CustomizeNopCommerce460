using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ProductOrderQty.Factory;
using Nop.Plugin.Widgets.ProductOrderQty.Models;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Catalog;

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
                var model = additionalData as ProductModel;

                if (model.Id > 0)
                {
                    var productExistance = await _productOrderQtyModelFactory.GetProductOrderQtyModelAsync(model.Id);

                    if (productExistance != null)
                    {
                        productExistance.ProductId = model.Id;
                        return View("~/Plugins/Widgets.ProductOrderQty/Views/CreateOrUpdate.cshtml", productExistance);
                    }
                    else
                    {
                        return View("~/Plugins/Widgets.ProductOrderQty/Views/CreateOrUpdate.cshtml", new ProductOrderQtyModel());
                    }
                }
                else
                {
                    return Content("");
                }
            }
            else if (widgetZone.Equals(PublicWidgetZones.ProductBoxAddinfoMiddle))
            {
                var model = additionalData as ProductOverviewModel;

                if (model.Id > 0)
                {
                    var productExistance = await _productOrderQtyModelFactory.GetProductOrderQtyModelAsync(model.Id);

                    if (productExistance != null)
                    {
                        productExistance.ProductId = model.Id;
                        return View("~/Plugins/Widgets.ProductOrderQty/Views/PublicView.cshtml", productExistance);
                    }
                    else
                    {
                        return View("~/Plugins/Widgets.ProductOrderQty/Views/PublicView.cshtml", new ProductOrderQtyModel());
                    }
                }
                else
                {
                    return Content("");
                }
            }
            else if (widgetZone.Equals(PublicWidgetZones.ProductPriceBottom) ||
                widgetZone.Equals(PublicWidgetZones.ProductDetailsBottom))
            {
                var model = additionalData as ProductDetailsModel;

                if (model.Id > 0)
                {
                    var productExistance = await _productOrderQtyModelFactory.GetProductOrderQtyModelAsync(model.Id);

                    if (productExistance != null)
                    {
                        productExistance.ProductId = model.Id;
                        return View("~/Plugins/Widgets.ProductOrderQty/Views/PublicView.cshtml", productExistance);
                    }
                    else
                    {
                        return View("~/Plugins/Widgets.ProductOrderQty/Views/PublicView.cshtml", new ProductOrderQtyModel());
                    }
                }
                else
                {
                    return Content("");
                }
            }
            else
            {
                return Content("");
            }
        }
    }
}
