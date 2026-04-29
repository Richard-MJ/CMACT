using Microsoft.EntityFrameworkCore;

namespace AutorizadorCanales.Infrastructure.Persistencia;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyHasTrigger(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null)
            {
                entityType.AddTrigger($"tr_bitacora_{tableName}_del");
                entityType.AddTrigger($"tr_bitacora_{tableName}_ins");
                entityType.AddTrigger($"tr_bitacora_{tableName}_upd");
            }
        }

        return modelBuilder;
    }
}
