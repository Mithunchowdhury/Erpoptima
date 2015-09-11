using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsDefectDetailMap : EntityTypeConfiguration<SlsDefectDetail>
    {
        public SlsDefectDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Reason).HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("SlsDefectDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsDefectId).HasColumnName("SlsDefectId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.ReplacedQuantity).HasColumnName("ReplacedQuantity");
            this.Property(t => t.AdjustedAmount).HasColumnName("AdjustedAmount");

            // Relationships
            this.HasRequired(t => t.SlsDefect)
                .WithMany(t => t.SlsDefectDetails)
                .HasForeignKey(d => d.SlsDefectId);
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.SlsDefectDetails)
                .HasForeignKey(d => d.SlsProductId);
            this.HasRequired(t => t.SlsUnit)
                .WithMany(t => t.SlsDefectDetails)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
