using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsReplacementDetailMap : EntityTypeConfiguration<SlsReplacementDetail>
    {
        public SlsReplacementDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Reason)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("SlsReplacementDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsReplacementId).HasColumnName("SlsReplacementId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.AdjustedAmount).HasColumnName("AdjustedAmount");
            this.Property(t => t.Reason).HasColumnName("Reason");

            // Relationships
            this.HasOptional(t => t.SlsProduct)
                .WithMany(t => t.SlsReplacementDetails)
                .HasForeignKey(d => d.SlsProductId);
            this.HasOptional(t => t.SlsReplacement)
                .WithMany(t => t.SlsReplacementDetails)
                .HasForeignKey(d => d.SlsReplacementId);
            this.HasOptional(t => t.SlsUnit)
                .WithMany(t => t.SlsReplacementDetails)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
