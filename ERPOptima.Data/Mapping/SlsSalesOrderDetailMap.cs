using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsSalesOrderDetailMap : EntityTypeConfiguration<SlsSalesOrderDetail>
    {
        public SlsSalesOrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsSalesOrderDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsSalesOrderId).HasColumnName("SlsSalesOrderId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.SalesOrderQuantity).HasColumnName("SalesOrderQuantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.Rate).HasColumnName("Rate");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.Total).HasColumnName("Total");

            // Relationships
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.SlsSalesOrderDetails)
                .HasForeignKey(d => d.SlsProductId);
            this.HasRequired(t => t.SlsSalesOrder)
                .WithMany(t => t.SlsSalesOrderDetails)
                .HasForeignKey(d => d.SlsSalesOrderId);
            this.HasRequired(t => t.SlsUnit)
                .WithMany(t => t.SlsSalesOrderDetails)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
