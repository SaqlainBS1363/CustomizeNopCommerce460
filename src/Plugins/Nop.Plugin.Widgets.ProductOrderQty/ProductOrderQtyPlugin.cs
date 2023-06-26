using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.ProductOrderQty.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.ProductOrderQty
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class ProductOrderQtyPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;

        public ProductOrderQtyPlugin(ILocalizationService localizationService,
            IWebHelper webHelper)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the widget zones
        /// </returns>
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { 
                AdminWidgetZones.ProductDetailsBlock, 
                PublicWidgetZones.ProductBoxAddinfoMiddle,
                PublicWidgetZones.ProductPriceBottom,
                PublicWidgetZones.ProductDetailsBottom
            });
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(ProductOrderQtyViewComponent);
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.ProductOrderQty.SaveNotification"] = "Product Order Quantity have been updated",
                ["Plugins.Widgets.ProductOrderQty.FormTitle"] = "Set Product Order Quantities",
                ["Plugins.Widgets.ProductOrderQty.FirstOrderQuantity"] = "First Order Quantity",
                ["Plugins.Widgets.ProductOrderQty.FirstOrderQuantity.Hint"] = "The quantity of a product one customer should must order if it's his/her first time ordering this product.",
                ["Plugins.Widgets.ProductOrderQty.ReOrderQuantity"] = "Reorder Quantity",
                ["Plugins.Widgets.ProductOrderQty.ReOrderQuantity.Hint"] = "Allowed number of quantity one customer can order if it is being re-ordered."
            });

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            //locales
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.ProductOrderQty");

            await base.UninstallAsync();
        }

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
    }
}