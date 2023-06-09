﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.VisitorsCrud.Domain;
using Nop.Plugin.Widgets.VisitorsCrud.Factory;
using Nop.Plugin.Widgets.VisitorsCrud.Models;
using Nop.Plugin.Widgets.VisitorsCrud.Service;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Seo;
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
        private readonly IUrlRecordService _urlRecordService;
        private readonly ILocalizedEntityService _localizedEntityService;



        public WidgetsVisitorsCrudController(ILocalizationService localizationService,
            INotificationService notificationService,
            IVisitorModelFactory visitorModelFactory,
            IUrlRecordService urlRecordService,
            IPermissionService permissionService,
            ILocalizedEntityService localizedEntityService)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _visitorModelFactory = visitorModelFactory;
            _urlRecordService = urlRecordService;
            _permissionService = permissionService;
            _localizedEntityService = localizedEntityService;
        }

        protected virtual async Task UpdateLocalesAsync(Visitor visitor, ConfigurationModel model)
        {
            if (model.Locales != null)
            {
                foreach (var localized in model.Locales)
                {
                    await _localizedEntityService.SaveLocalizedValueAsync(visitor,
                        x => x.Name,
                        localized.Name,
                        localized.LanguageId);

                    //search engine name
                    var seName = await _urlRecordService.ValidateSeNameAsync(visitor, localized.SeName, localized.Name, false);
                    await _urlRecordService.SaveSlugAsync(visitor, seName, localized.LanguageId);
                }
            }
        }

        public virtual async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //prepare model
            var model = await _visitorModelFactory.PrepareVisitorSearchModelAsync(new ConfigurationSearchModel());

            return View("~/Plugins/Widgets.VisitorsCrud/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Configure(ConfigurationSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _visitorModelFactory.PrepareVisitorModelListAsync(searchModel);

            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = await _visitorModelFactory.PrepareVisitorModelAsync(new ConfigurationModel(), null);

            return View("~/Plugins/Widgets.VisitorsCrud/Views/Create.cshtml", model);
        }

        [HttpPost]
        [FormValueRequired("save")]
        public async Task<IActionResult> Create(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var visitor = await _visitorModelFactory.AddVisitorModelAsync(model);

                //search engine name
                model.SeName = "" + model.Name + model.Age;
                model.SeName = await _urlRecordService.ValidateSeNameAsync(visitor, model.SeName, visitor.Name, true);
                await _urlRecordService.SaveSlugAsync(visitor, model.SeName, 0);

                //locales
                await UpdateLocalesAsync(visitor, model);

                ViewBag.RefreshPage = true;

                var model1 = await _visitorModelFactory.PrepareVisitorModelAsync(model);

                return View("~/Plugins/Widgets.VisitorsCrud/Views/Create.cshtml", model1);
            }

            /*_notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));*/

            return RedirectToAction("Configure");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var visitor = await _visitorModelFactory.GetVisitorAsync(Id);

            //prepare model
            var model = await _visitorModelFactory.PrepareVisitorModelAsync(null, visitor);

            return View("~/Plugins/Widgets.VisitorsCrud/Views/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var visitor = await _visitorModelFactory.EditVisitorModelAsync(model);

                //search engine name
                model.SeName = "" + model.Name + model.Age;
                model.SeName = await _urlRecordService.ValidateSeNameAsync(visitor, model.SeName, visitor.Name, true);
                await _urlRecordService.SaveSlugAsync(visitor, model.SeName, 0);

                //locales
                await UpdateLocalesAsync(visitor, model);

                ViewBag.RefreshPage = true;

                var model1 = await _visitorModelFactory.PrepareVisitorModelAsync(model);

                return View("~/Plugins/Widgets.VisitorsCrud/Views/Edit.cshtml", model1);
            }

            /*_notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));*/

            return RedirectToAction("Configure");
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

            /*_notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));*/

            return RedirectToAction("Configure");
        }
    }
}