using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.VisitorsCrud.Factory;
using Nop.Plugin.Widgets.VisitorsCrud.Models;
using Nop.Plugin.Widgets.VisitorsCrud.Service;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.VisitorsCrud.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsVisitorsCrudController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IVisitorModelFactory _visitorModelFactory;
        private readonly IVisitorService _visitorService;

        public WidgetsVisitorsCrudController(ILocalizationService localizationService,
            INotificationService notificationService,
            IVisitorModelFactory visitorModelFactory,
            IVisitorService visitorService,
            IPermissionService permissionService, 
            IPictureService pictureService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _visitorModelFactory = visitorModelFactory;
            _visitorService = visitorService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = await _visitorModelFactory.PrepareVisitorModelListAsync();

            return View("~/Plugins/Widgets.VisitorsCrud/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var simplePluginSettings = await _settingService.LoadSettingAsync<VisitorsCrudSettings>(storeScope);

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