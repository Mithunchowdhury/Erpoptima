using ERPOptima.Model.Inventory;
using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvStoreOpeningMap : EntityTypeConfiguration<InvStoreOpening>
    {
        public InvStoreOpeningMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("InvStoreOpenings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvStoreId).HasColumnName("InvStoreId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.InvStore)
                .WithMany(t => t.InvStoreOpenings)
                .HasForeignKey(d => d.InvStoreId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.InvStoreOpenings)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.InvStoreOpenings1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SlsProduct)
                .WithMany(t => t.InvStoreOpenings)
                .HasForeignKey(d => d.SlsProductId);
            this.HasOptional(t => t.SlsUnit)
                .WithMany(t => t.InvStoreOpenings)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
