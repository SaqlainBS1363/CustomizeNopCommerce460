using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.PlugSimple.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.PlugSimple.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsPlugSimpleController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public WidgetsPlugSimpleController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService, 
            IPictureService pictureService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var simplePluginSettings = await _settingService.LoadSettingAsync<PlugSimpleSettings>(storeScope);
            var model = new ConfigurationModel
            {
                PS_PictureId = simplePluginSettings.PS_PictureId,
                PS_Text = simplePluginSettings.PS_Text,
                PS_Link = simplePluginSettings.PS_Link,
                PS_AltText = simplePluginSettings.PS_AltText
            };

            if (storeScope > 0)
            {
                model.PS_PictureId_OverrideForStore = await _settingService.SettingExistsAsync(simplePluginSettings, x => x.PS_PictureId, storeScope);
                model.PS_Text_OverrideForStore = await _settingService.SettingExistsAsync(simplePluginSettings, x => x.PS_Text, storeScope);
                model.PS_Link_OverrideForStore = await _settingService.SettingExistsAsync(simplePluginSettings, x => x.PS_Link, storeScope);
                model.PS_AltText_OverrideForStore = await _settingService.SettingExistsAsync(simplePluginSettings, x => x.PS_AltText, storeScope);
            }

            return View("~/Plugins/Widgets.PlugSimple/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var simplePluginSettings = await _settingService.LoadSettingAsync<PlugSimpleSettings>(storeScope);

            //get previous picture identifiers
            var previousPictureIds = new[] 
            {
                simplePluginSettings.PS_PictureId
            };

            simplePluginSettings.PS_PictureId = model.PS_PictureId;
            simplePluginSettings.PS_Text = model.PS_Text;
            simplePluginSettings.PS_Link = model.PS_Link;
            simplePluginSettings.PS_AltText = model.PS_AltText;
            

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            await _settingService.SaveSettingOverridablePerStoreAsync(simplePluginSettings, x => x.PS_PictureId, model.PS_PictureId_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(simplePluginSettings, x => x.PS_Text, model.PS_Text_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(simplePluginSettings, x => x.PS_Link, model.PS_Link_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(simplePluginSettings, x => x.PS_AltText, model.PS_AltText_OverrideForStore, storeScope, false);
            
            //now clear settings cache
            await _settingService.ClearCacheAsync();
            
            //get current picture identifiers
            var currentPictureIds = new[]
            {
                simplePluginSettings.PS_PictureId
            };

            //delete an old picture (if deleted or updated)
            foreach (var pictureId in previousPictureIds.Except(currentPictureIds))
            { 
                var previousPicture = await _pictureService.GetPictureByIdAsync(pictureId);
                if (previousPicture != null)
                    await _pictureService.DeletePictureAsync(previousPicture);
            }

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
            
            return await Configure();
        }
    }
}