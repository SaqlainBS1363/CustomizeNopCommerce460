using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.ProductOrderQty.Domain;

namespace Nop.Plugin.Widgets.ProductOrderQty.Service
{
    public interface IProductOrderQtyService
    {
        // Get ProductOrderQtyEntity
        Task<ProductOrderQtyEntity> GetProductOrderQtyEntityAsync(int productId);

        //Add ProductOrderQtyEntity
        Task AddProductOrderQtyEntityAsync(ProductOrderQtyEntity productOrderQtyEntity);

        //Update or Edit ProductOrderQtyEntity
        Task UpdateProductOrderQtyEntityAsync(ProductOrderQtyEntity productOrderQtyEntity);

        //Delete ProductOrderQtyEntity
        Task DeleteProductOrderQtyEntityAsync(ProductOrderQtyEntity productOrderQtyEntity);
    }
}
