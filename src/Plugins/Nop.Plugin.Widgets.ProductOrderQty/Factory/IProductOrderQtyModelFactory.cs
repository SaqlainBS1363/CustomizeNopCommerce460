using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.ProductOrderQty.Domain;
using Nop.Plugin.Widgets.ProductOrderQty.Models;
using Nop.Web.Models.Customer;

namespace Nop.Plugin.Widgets.ProductOrderQty.Factory
{
    public interface IProductOrderQtyModelFactory
    {
        Task<ProductOrderQtyModel> GetProductOrderQtyModelAsync(int ProductId);

        Task<ProductOrderQtyEntity> AddProductOrderQtyModelAsync(ProductOrderQtyModel productOrderQtyModel);

        Task<ProductOrderQtyEntity> EditProductOrderQtyModelAsync(ProductOrderQtyModel productOrderQtyModel);

        Task<ProductOrderQtyEntity> DeleteProductOrderQtyModelAsync(int ProductId);
    }
}
