using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsPromotionalOfferMap : EntityTypeConfiguration<SlsPromotionalOffer>
    {
        public SlsPromotionalOfferMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Remarks)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SlsPromotionalOffers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.SlsRegionId).HasColumnName("SlsRegionId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsPromotionalOffers)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsPromotionalOffers1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SlsRegion)
                .WithMany(t => t.SlsPromotionalOffers)
                .HasForeignKey(d => d.SlsRegionId);

        }
    }
}
