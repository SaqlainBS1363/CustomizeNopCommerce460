using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.VisitorsCrud.Models
{
    /// <summary>
    /// Represents a visitor model
    /// </summary>
    public partial record ConfigurationModel : BaseNopEntityModel, ILocalizedModel<VisitorLocalizedModel>
    {
        [NopResourceDisplayName("Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Age")]
        public int Age { get; set; }

        public string SeName { get; set; }

        [NopResourceDisplayName("Gender")]
        public string Gender { get; set; }
        public IEnumerable<SelectListItem> GenderSelection { get; set; }

        [DataType(DataType.PhoneNumber)]
        [NopResourceDisplayName("Phone")]
        public string Phone { get; set; }

        [NopResourceDisplayName("Active")]
        public bool IsActive { get; set; }

        public IList<VisitorLocalizedModel> Locales { get; set; }

    }

    public partial record VisitorLocalizedModel : ILocalizedLocaleModel
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Name")]
        public string Name { get; set; }

        public string SeName { get; set; }
    }
}