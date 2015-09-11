using ERPOptima.Model.Sales; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsFreeProductMap : EntityTypeConfiguration<SlsFreeProduct>
    {
        public SlsFreeProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                       

            // Table & Column Mappings
            this.ToTable("SlsFreeProducts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.MeasurementQuantity).HasColumnName("MeasurementQuantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.FreeQuantity).HasColumnName("FreeQuantity");
            this.Property(t => t.FreeUnitId).HasColumnName("FreeUnitId");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.SecCompnayId).HasColumnName("SecCompnayId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsFreeProducts)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsFreeProducts1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.SlsFreeProducts)
                .HasForeignKey(d => d.SlsProductId);
            this.HasRequired(t => t.SlsUnit)
                .WithMany(t => t.SlsFreeProducts)
                .HasForeignKey(d => d.SlsUnitId);
            this.HasRequired(t => t.SlsUnit1)
                .WithMany(t => t.SlsFreeProducts1)
                .HasForeignKey(d => d.FreeUnitId).WillCascadeOnDelete(false);

        }
    }
}
