using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Data;
using Nop.Plugin.Widgets.ProductOrderQty.Domain;

namespace Nop.Plugin.Widgets.ProductOrderQty.Service
{
    public class ProductOrderQtyService : IProductOrderQtyService
    {
        private readonly IRepository<ProductOrderQtyEntity> _productOrderQtyRepository;

        public ProductOrderQtyService(IRepository<ProductOrderQtyEntity> productOrderQtyRepository)
        {
            _productOrderQtyRepository = productOrderQtyRepository;
        }

        public async Task<ProductOrderQtyEntity> GetProductOrderQtyEntityAsync(int productId)
        {
            var productOrderQty = await _productOrderQtyRepository.GetAllAsync(query =>
            {
                query = query.Where(c => c.ProductId == productId);

                return query;
            });

            if(productOrderQty == null)
            {
                return null;
            }

            return productOrderQty[0];
        }

        public async Task AddProductOrderQtyEntityAsync(ProductOrderQtyEntity productOrderQty)
        {
            await _productOrderQtyRepository.InsertAsync(productOrderQty);
        }

        public async Task DeleteProductOrderQtyEntityAsync(ProductOrderQtyEntity productOrderQty)
        {
            await _productOrderQtyRepository.DeleteAsync(productOrderQty);
        }

        public async Task UpdateProductOrderQtyEntityAsync(ProductOrderQtyEntity productOrderQty)
        {
            await _productOrderQtyRepository.UpdateAsync(productOrderQty);
        }
    }
}
