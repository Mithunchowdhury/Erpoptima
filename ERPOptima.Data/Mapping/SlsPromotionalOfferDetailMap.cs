using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsPromotionalOfferDetailMap : EntityTypeConfiguration<SlsPromotionalOfferDetail>
    {
        public SlsPromotionalOfferDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsPromotionalOfferDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsPromotionalOfferId).HasColumnName("SlsPromotionalOfferId");
            this.Property(t => t.SlsProuctId).HasColumnName("SlsProuctId");
            this.Property(t => t.Discount).HasColumnName("Discount");

            // Relationships
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.SlsPromotionalOfferDetails)
                .HasForeignKey(d => d.SlsProuctId);
            this.HasRequired(t => t.SlsPromotionalOffer)
                .WithMany(t => t.SlsPromotionalOfferDetails)
                .HasForeignKey(d => d.SlsPromotionalOfferId);

        }
    }
}
