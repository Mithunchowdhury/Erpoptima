using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsSalesReturnDetailMap : EntityTypeConfiguration<SlsSalesReturnDetail>
    {
        public SlsSalesReturnDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsSalesReturnDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsReturnId).HasColumnName("SlsReturnId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.ReturnedQuantity).HasColumnName("ReturnedQuantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");

            // Relationships
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.SlsSalesReturnDetails)
                .HasForeignKey(d => d.SlsProductId);
            this.HasRequired(t => t.SlsSalesReturn)
                .WithMany(t => t.SlsSalesReturnDetails)
                .HasForeignKey(d => d.SlsReturnId);
            this.HasRequired(t => t.SlsUnit)
                .WithMany(t => t.SlsSalesReturnDetails)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
