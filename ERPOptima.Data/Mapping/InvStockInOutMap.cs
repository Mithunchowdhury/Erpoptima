using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvStockInOutMap : EntityTypeConfiguration<InvStockInOut>
    {
        public InvStockInOutMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("InvStockInOuts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvStoreId).HasColumnName("InvStoreId");
            this.Property(t => t.TransactionType).HasColumnName("TransactionType");
            this.Property(t => t.RefId).HasColumnName("RefId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");

            // Relationships
            this.HasOptional(t => t.InvStore)
                .WithMany(t => t.InvStockInOuts)
                .HasForeignKey(d => d.InvStoreId);
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.InvStockInOuts)
                .HasForeignKey(d => d.SlsProductId);
            this.HasOptional(t => t.SlsUnit)
                .WithMany(t => t.InvStockInOuts)
                .HasForeignKey(d => d.SlsUnitId);


        }
    }
}
