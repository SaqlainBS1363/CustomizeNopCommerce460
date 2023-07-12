using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.ProductOrderQty.Domain;

namespace Nop.Plugin.Widgets.ProductOrderQty.Data.Migrations
{
    [NopMigration("2023-06-22 15:45:00:9043677", "Product order quantity base schema", MigrationProcessType.Installation)]
    public class ProductOrderQtyDBMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<ProductOrderQtyEntity>();
        }
    }
}
