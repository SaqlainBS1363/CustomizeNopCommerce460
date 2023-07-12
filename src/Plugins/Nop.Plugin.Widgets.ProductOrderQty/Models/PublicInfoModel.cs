using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.ProductOrderQty.Models
{
    public record PublicInfoModel : BaseNopModel
    {
        public int FirstOrderQty { get; set; }
        public int ReOrderQty { get; set; }
    }
}