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
        private readonly IVisitorModelFactory _visitorModelFactory;

        public WidgetsVisitorsCrudController(ILocalizationService localizationService,
            INotificationService notificationService,
            IVisitorModelFactory visitorModelFactory,
            IPermissionService permissionService)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _visitorModelFactory = visitorModelFactory;
            _permissionService = permissionService;
        }

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = await _visitorModelFactory.PrepareVisitorModelListAsync();

            return View("~/Plugins/Widgets.VisitorsCrud/Views/Configure.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = await _visitorModelFactory.PrepareVisitorModelAsync(new ConfigurationModel());

            return View("~/Plugins/Widgets.VisitorsCrud/Views/Create.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            await _visitorModelFactory.AddVisitorModelAsync(model);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
            
            return await Configure();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var visitor = await _visitorModelFactory.GetVisitorModelAsync(Id);

            return View("~/Plugins/Widgets.VisitorsCrud/Views/Edit.cshtml", visitor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            await _visitorModelFactory.EditVisitorModelAsync(model);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return await Configure();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var visitor = await _visitorModelFactory.GetVisitorModelAsync(Id);

            return View("~/Plugins/Widgets.VisitorsCrud/Views/Delete.cshtml", visitor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            await _visitorModelFactory.DeleteVisitorModelAsync(Id);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return await Configure();
        }
    }
}