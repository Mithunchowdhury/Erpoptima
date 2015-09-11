using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsProductUnitMap : EntityTypeConfiguration<SlsProductUnit>
    {
        public SlsProductUnitMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsProductUnits");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.ParentUnitId).HasColumnName("ParentUnitId");
            this.Property(t => t.ConversionRate).HasColumnName("ConversionRate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsProductUnits)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsProductUnits1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SlsUnit)
                .WithMany(t => t.SlsProductUnits)
                .HasForeignKey(d => d.SlsUnitId);
            this.HasOptional(t => t.SlsUnit1)
                .WithMany(t => t.SlsProductUnits1)
                .HasForeignKey(d => d.ParentUnitId);

        }
    }
}
