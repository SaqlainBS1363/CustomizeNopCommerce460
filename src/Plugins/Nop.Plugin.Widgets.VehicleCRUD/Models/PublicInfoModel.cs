using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.VisitorsCrud.Models
{
    public record PublicInfoModel : BaseNopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
    }
}