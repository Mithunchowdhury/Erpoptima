using ERPOptima.Model.Sales; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsGeneralDiscountMap : EntityTypeConfiguration<SlsGeneralDiscount>
    {
        public SlsGeneralDiscountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsGeneralDiscounts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsRegionId).HasColumnName("SlsRegionId");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsGeneralDiscounts)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsGeneralDiscounts1)
                .HasForeignKey(d => d.ModifiedBy);            
            this.HasRequired(t => t.SlsRegion)
                .WithMany(t => t.SlsGeneralDiscounts)
                .HasForeignKey(d => d.SlsRegionId);
           

        }
    }
}
