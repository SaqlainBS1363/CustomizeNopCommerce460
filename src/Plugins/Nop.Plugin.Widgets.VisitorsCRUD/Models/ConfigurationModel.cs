﻿using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.VisitorsCrud.Models
{
    public partial record ConfigurationModel : BaseNopEntityModel
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
    }
}