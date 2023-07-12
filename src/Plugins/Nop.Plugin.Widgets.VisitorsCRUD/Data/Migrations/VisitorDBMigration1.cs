using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.VisitorsCrud.Domain;

namespace Nop.Plugin.Widgets.VisitorsCrud.Data.Migrations
{
    [NopMigration("2023-06-13 15:45:00:9043077", "Custom Visitor base schema update col", MigrationProcessType.Update)]
    public class VisitorDBMigration1 : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column(nameof(Visitor.IsActive))
                .OnTable(nameof(Visitor))
                .AsBoolean()
                .Nullable();
        }
    }
}
