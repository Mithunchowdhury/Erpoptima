using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsSalesTargetDetailMap : EntityTypeConfiguration<SlsSalesTargetDetail>
    {
        public SlsSalesTargetDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsSalesTargetDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsSalesTargetId).HasColumnName("SlsSalesTargetId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");

            // Relationships
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.SlsSalesTargetDetails)
                .HasForeignKey(d => d.SlsProductId);
            this.HasRequired(t => t.SlsSalesTarget)
                .WithMany(t => t.SlsSalesTargetDetails)
                .HasForeignKey(d => d.SlsSalesTargetId);
            this.HasRequired(t => t.SlsUnit)
                .WithMany(t => t.SlsSalesTargetDetails)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
