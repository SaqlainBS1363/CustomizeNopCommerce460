using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.VisitorsCrud.Models
{
    public partial record ConfigurationSearchModel : BaseSearchModel
    {
        [NopResourceDisplayName("Visitor Name")]
        public string SearchVisitorName { get; set; }

        /*public string SearchVisitorGender { get; set; }

        public IList<SelectListItem> AvailableVisitorGenderOptions { get; set; }*/
    }
}
