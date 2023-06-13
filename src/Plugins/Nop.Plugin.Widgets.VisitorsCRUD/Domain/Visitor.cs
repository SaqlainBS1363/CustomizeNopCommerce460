using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Seo;

namespace Nop.Plugin.Widgets.VisitorsCrud.Domain
{
    public class Visitor : BaseEntity, ISlugSupported
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
