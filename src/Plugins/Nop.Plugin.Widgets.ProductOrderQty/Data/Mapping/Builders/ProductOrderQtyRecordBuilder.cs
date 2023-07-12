using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.ProductOrderQty.Domain;

namespace Nop.Plugin.Widgets.ProductOrderQty.Data.Mapping.Builders
{
    public class ProductOrderQtyRecordBuilder : NopEntityBuilder<ProductOrderQtyEntity>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(ProductOrderQtyEntity.ProductId)).AsInt64()
            .WithColumn(nameof(ProductOrderQtyEntity.FirstOrderQty)).AsInt64()
            .WithColumn(nameof(ProductOrderQtyEntity.ReOrderQty)).AsInt64();
        }
    }
}
