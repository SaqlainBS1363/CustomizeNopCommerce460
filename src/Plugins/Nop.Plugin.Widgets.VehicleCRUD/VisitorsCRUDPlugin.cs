using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.VisitorsCrud.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.VisitorsCrud
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class VisitorsCrudPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;

        public VisitorsCrudPlugin(ILocalizationService localizationService,
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
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsVisitorsCrud/Configure";
        }

        /// <summary>
        /// Manage sitemap. You can use "SystemName" of menu items to manage existing sitemap or add a new menu item.
        /// </summary>
        /// <param name="rootNode">Root node of the sitemap.</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var index = rootNode.ChildNodes.Count;

            rootNode.ChildNodes.Insert(index, new SiteMapNode
            {
                Visible = true,
                SystemName = "Widgets.VisitorsCrud",
                Title = "Visitors Crud",
                IconClass = "far fa-dot-circle",
                ControllerName = "WidgetsVisitorsCrud",
                ActionName = "Configure",
                RouteValues = new RouteValueDictionary { { "area", AreaNames.Admin } }
            });
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(WidgetsVisitorsCrudViewComponent);
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.VisitorsCrud.AddVisitorButton"] = "Add Visitor",
                ["Plugins.Widgets.VisitorsCrud.EditVisitorButton"] = "Edit",
                ["Plugins.Widgets.VisitorsCrud.DeleteVisitorButton"] = "Delete",
                ["Plugins.Widgets.VisitorsCrud.SaveVisitorInfo"] = "Save",
                ["Plugins.Widgets.VisitorsCrud.IndexPageTitle"] = "Visitor Info Page",
                ["Plugins.Widgets.VisitorsCrud.EditPageTitle"] = "Edit VIsitor"                
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
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.VisitorsCrud");

            await base.UninstallAsync();
        }

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
    }
}