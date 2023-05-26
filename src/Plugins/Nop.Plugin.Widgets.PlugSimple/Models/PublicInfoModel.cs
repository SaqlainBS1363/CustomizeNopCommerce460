using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.PlugSimple.Models
{
    public record PublicInfoModel : BaseNopModel
    {
        public string PS_PictureUrl { get; set; }
        public string PS_Text { get; set; }
        public string PS_Link { get; set; }
        public string PS_AltText { get; set; }
    }
}