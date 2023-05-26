using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.PlugSimple.Models
{
    public record ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }
        
        [NopResourceDisplayName("Plugins.Widgets.PlugSimple.Picture")]
        [UIHint("Picture")]
        public int PS_PictureId { get; set; }
        public bool PS_PictureId_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PlugSimple.Text")]
        public string PS_Text { get; set; }
        public bool PS_Text_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PlugSimple.Link")]
        public string PS_Link { get; set; }
        public bool PS_Link_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PlugSimple.AltText")]
        public string PS_AltText { get; set; }
        public bool PS_AltText_OverrideForStore { get; set; }
    }
}