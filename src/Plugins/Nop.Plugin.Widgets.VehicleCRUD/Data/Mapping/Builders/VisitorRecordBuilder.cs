using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.VisitorsCrud.Domain;

namespace Nop.Plugin.Widgets.VisitorsCrud.Data.Mapping.Builders
{
    public class VisitorRecordBuilder : NopEntityBuilder<Visitor>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Visitor.Name)).AsString(100)
            .WithColumn(nameof(Visitor.Age)).AsInt64()
            .WithColumn(nameof(Visitor.Gender)).AsString(10)
            .WithColumn(nameof(Visitor.Phone)).AsString(20);
        }
    }
}
