using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.ProductOrderQty.Models
{
    /// <summary>
    /// Represents a product order quantity model
    /// </summary>
    public partial record ProductOrderQtyModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.ProductOrderQty.FirstOrderQuantity")]
        public int FirstOrderQty { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.ProductOrderQty.ReOrderQuantity")]
        public int ReOrderQty { get; set; }
    }
}