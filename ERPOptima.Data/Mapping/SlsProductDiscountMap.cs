using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsProductDiscountMap : EntityTypeConfiguration<SlsProductDiscount>
    {
        public SlsProductDiscountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsProductDiscounts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsRegionId).HasColumnName("SlsRegionId");
            this.Property(t => t.SlsProuctId).HasColumnName("SlsProuctId");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // Relationships
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.SlsProductDiscounts)
                .HasForeignKey(d => d.SlsProuctId);
            this.HasRequired(t => t.SlsRegion)
                .WithMany(t => t.SlsProductDiscounts)
                .HasForeignKey(d => d.SlsRegionId);

        }
    }
}
