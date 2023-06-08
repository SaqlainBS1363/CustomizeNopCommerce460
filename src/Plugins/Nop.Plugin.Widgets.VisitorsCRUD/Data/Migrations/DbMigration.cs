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
    [NopMigration("2023-06-08 19:45:00:9043677", "Custom Visitor base schema", MigrationProcessType.Installation)]
    public class DbMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<Visitor>();
        }
    }
}
