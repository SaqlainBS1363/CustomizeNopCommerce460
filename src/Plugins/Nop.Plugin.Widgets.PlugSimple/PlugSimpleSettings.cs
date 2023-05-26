using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.PlugSimple
{
    public class PlugSimpleSettings : ISettings
    {
        public int PS_PictureId { get; set; }
        public string PS_Text { get; set; }
        public string PS_Link { get; set; }
        public string PS_AltText { get; set; }
    }
}