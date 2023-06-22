using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;

namespace Nop.Plugin.Widgets.ProductOrderQty.Domain
{
    public class ProductOrderQtyEntity : BaseEntity
    {
        public int ProductId { get; set; }
        public int FirstOrderQty { get; set; }
        public int ReOrderQty { get; set; }
    }
}
