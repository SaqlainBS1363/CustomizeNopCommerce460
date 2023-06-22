using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ProductOrderQty.Domain;
using Nop.Plugin.Widgets.ProductOrderQty.Factory;
using Nop.Plugin.Widgets.ProductOrderQty.Models;
using Nop.Plugin.Widgets.ProductOrderQty.Service;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.ProductOrderQty.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class ProductOrderQtyController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IProductOrderQtyModelFactory _productOrderQtyModelFactory;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ILocalizedEntityService _localizedEntityService;


        public ProductOrderQtyController(ILocalizationService localizationService,
            INotificationService notificationService,
            IProductOrderQtyModelFactory productOrderQtyModelFactory,
            IUrlRecordService urlRecordService,
            IPermissionService permissionService,
            ILocalizedEntityService localizedEntityService)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _productOrderQtyModelFactory = productOrderQtyModelFactory;
            _urlRecordService = urlRecordService;
            _permissionService = permissionService;
            _localizedEntityService = localizedEntityService;
        }


        /*[HttpGet]
        public async Task<IActionResult> CreateOrUpdate()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new ProductOrderQtyModel();

            return View(model);
        }*/

        /*[HttpPost]
        [FormValueRequired("save")]
        public async Task<IActionResult> CreateOrUpdate(ProductOrderQtyModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();


        }*/

        /*[HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var visitor = await _productOrderQtyModelFactory.GetProductOrderQtyModelAsync(Id);

            return View(visitor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductOrderQtyModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var visitor = await _productOrderQtyModelFactory.EditProductOrderQtyModelAsync(model);

                return View(visitor);
            }

            return RedirectToAction("Configure");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var visitor = await _productOrderQtyModelFactory.GetVisitorModelAsync(Id);

            return View("~/Plugins/Widgets.VisitorsCrud/Views/Delete.cshtml", visitor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            await _productOrderQtyModelFactory.DeleteVisitorModelAsync(Id);

            *//*_notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));*//*

            return RedirectToAction("Configure");
        }*/
    }
}