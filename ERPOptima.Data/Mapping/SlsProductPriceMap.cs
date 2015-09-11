using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsProductPriceMap : EntityTypeConfiguration<SlsProductPrice>
    {
        public SlsProductPriceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsProductPrices");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.FactoryCost).HasColumnName("FactoryCost");
            this.Property(t => t.MRP).HasColumnName("MRP");
            this.Property(t => t.DistributorCommission).HasColumnName("DistributorCommission");
            this.Property(t => t.RetailCommission).HasColumnName("RetailCommission");
            this.Property(t => t.DeclarationDate).HasColumnName("DeclarationDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsProductPrices)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsProductPrices1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.SlsProductPrices)
                .HasForeignKey(d => d.SlsProductId);
            this.HasRequired(t => t.SlsUnit)
                .WithMany(t => t.SlsProductPrices)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
