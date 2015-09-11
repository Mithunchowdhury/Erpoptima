using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvProductSendDetailItemMap : EntityTypeConfiguration<InvProductSendDetailItem>
    {
        public InvProductSendDetailItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("InvProductSendDetailItems");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvProductSendDetailId).HasColumnName("InvProductSendDetailId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.InvProductSendDetail)
                .WithMany(t => t.InvProductSendDetailItems)
                .HasForeignKey(d => d.InvProductSendDetailId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.InvProductSendDetailItems)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.InvProductSendDetailItems1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
