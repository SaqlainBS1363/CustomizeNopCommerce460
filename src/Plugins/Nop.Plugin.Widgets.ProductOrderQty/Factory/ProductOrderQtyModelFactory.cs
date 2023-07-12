using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.ProductOrderQty.Domain;
using Nop.Plugin.Widgets.ProductOrderQty.Models;
using Nop.Plugin.Widgets.ProductOrderQty.Service;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Models.Customer;

namespace Nop.Plugin.Widgets.ProductOrderQty.Factory
{
    public class ProductOrderQtyModelFactory : IProductOrderQtyModelFactory
    {
        private readonly IProductOrderQtyService _productOrderQtyService;

        public ProductOrderQtyModelFactory(IProductOrderQtyService productOrderQtyService)
        {
            _productOrderQtyService = productOrderQtyService;
        }

        public async Task<ProductOrderQtyModel> GetProductOrderQtyModelAsync(int ProductId)
        {
            var getProductOrderQty = _productOrderQtyService.GetProductOrderQtyEntityAsync(ProductId).Result;

            if(getProductOrderQty == null)
            {
                return null;
            }

            var sendProductOrderQty = new ProductOrderQtyModel
            {
                Id = getProductOrderQty.Id,
                ProductId = getProductOrderQty.ProductId,
                FirstOrderQty = getProductOrderQty.FirstOrderQty,
                ReOrderQty = getProductOrderQty.ReOrderQty
            };
            return sendProductOrderQty;
        }

        public async Task<ProductOrderQtyEntity> AddProductOrderQtyModelAsync(ProductOrderQtyModel productOrderQtyModel)
        {
            var newProductOrderQty = new ProductOrderQtyEntity();

            newProductOrderQty.ProductId = productOrderQtyModel.ProductId;
            newProductOrderQty.FirstOrderQty = productOrderQtyModel.FirstOrderQty;
            newProductOrderQty.ReOrderQty = productOrderQtyModel.ReOrderQty;

            await _productOrderQtyService.AddProductOrderQtyEntityAsync(newProductOrderQty);

            return newProductOrderQty;
        }

        public async Task<ProductOrderQtyEntity> EditProductOrderQtyModelAsync(ProductOrderQtyModel productOrderQtyModel)
        {
            var newProductOrderQty = new ProductOrderQtyEntity();

            newProductOrderQty.Id = productOrderQtyModel.Id;
            newProductOrderQty.ProductId = productOrderQtyModel.ProductId;
            newProductOrderQty.FirstOrderQty = productOrderQtyModel.FirstOrderQty;
            newProductOrderQty.ReOrderQty= productOrderQtyModel.ReOrderQty;

            await _productOrderQtyService.UpdateProductOrderQtyEntityAsync(newProductOrderQty);

            return newProductOrderQty;
        }

        public async Task<ProductOrderQtyEntity> DeleteProductOrderQtyModelAsync(int ProductId)
        {
            var newProductOrderQty = _productOrderQtyService.GetProductOrderQtyEntityAsync(ProductId).Result;

            await _productOrderQtyService.DeleteProductOrderQtyEntityAsync(newProductOrderQty);

            return null;
        }
    }
}
