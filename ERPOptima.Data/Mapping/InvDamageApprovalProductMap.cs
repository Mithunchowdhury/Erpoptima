using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvDamageApprovalProductMap : EntityTypeConfiguration<InvDamageApprovalProduct>
    {
        public InvDamageApprovalProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("InvDamageApprovalProducts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvDamageId).HasColumnName("InvDamageId");
            this.Property(t => t.DamagedQuantity).HasColumnName("DamagedQuantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.ApprovedQuantity).HasColumnName("ApprovedQuantity");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.InvDamage)
                .WithMany(t => t.InvDamageApprovalProducts)
                .HasForeignKey(d => d.InvDamageId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.InvDamageApprovalProducts)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.InvDamageApprovalProducts1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
