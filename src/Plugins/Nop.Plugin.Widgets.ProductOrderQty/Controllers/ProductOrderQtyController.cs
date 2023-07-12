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
        private readonly IPermissionService _permissionService;
        private readonly IProductOrderQtyModelFactory _productOrderQtyModelFactory;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;

        public ProductOrderQtyController(IProductOrderQtyModelFactory productOrderQtyModelFactory,
            IPermissionService permissionService,
            INotificationService notificationService,
            ILocalizationService localizationService)
        {
            _productOrderQtyModelFactory = productOrderQtyModelFactory;
            _permissionService = permissionService;
            _notificationService = notificationService;
            _localizationService = localizationService;
        }


        /*[HttpGet]
        public async Task<IActionResult> CreateOrUpdate()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new ProductOrderQtyModel();

            return View(model);
        }*/

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(int productId, int firstOrderQty, int reOrderQty)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            var checkProduct = await _productOrderQtyModelFactory.GetProductOrderQtyModelAsync(productId);

            var model = new ProductOrderQtyModel
            {
                ProductId = productId,
                FirstOrderQty = firstOrderQty,
                ReOrderQty = reOrderQty
            };

            if (checkProduct.Id > 0)
            {
                model.Id = checkProduct.Id;
                await _productOrderQtyModelFactory.EditProductOrderQtyModelAsync(model);
                //_notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Plugins.Widgets.ProductOrderQty.SaveNotification"));
            }
            else
            {
                await _productOrderQtyModelFactory.AddProductOrderQtyModelAsync(model);
                //_notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Plugins.Widgets.ProductOrderQty.SaveNotification"));
            }

            return Json(new { Result = true });
        }
    }
}