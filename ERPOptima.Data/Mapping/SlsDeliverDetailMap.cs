using ERPOptima.Model.Sales;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPOptima.Data.Mapping
{
    public class SlsDeliverDetailMap : EntityTypeConfiguration<SlsDeliverDetail>
    {
        public SlsDeliverDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsDeliveryDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsDeliveryId).HasColumnName("SlsDeliveryId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.Rate).HasColumnName("Rate");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.Total).HasColumnName("Total");

            // Relationships
            this.HasRequired(t => t.SlsDelivery)
                .WithMany(t => t.SlsDeliverDetails)
                .HasForeignKey(d => d.SlsDeliveryId);

        }
    }
}
