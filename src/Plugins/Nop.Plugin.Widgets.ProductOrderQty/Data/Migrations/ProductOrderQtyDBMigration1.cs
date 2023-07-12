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
    [NopMigration("2023-06-22 15:45:00:9043077", "Product Order Quantity settings", MigrationProcessType.Update)]
    public class ProductOrderQtyDBMigration1 : AutoReversingMigration
    {
        public override void Up()
        {
            /*Create.Column(nameof(ProductOrderQtyEntity.xyz))
                .OnTable(nameof(ProductOrderQtyEntity))
                .AsBoolean()
                .Nullable();*/
        }
    }
}
