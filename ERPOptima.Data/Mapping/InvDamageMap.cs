using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvDamageMap : EntityTypeConfiguration<InvDamage>
    {
        public InvDamageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("InvDamages");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.InvStoreId).HasColumnName("InvStoreId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ApprovalStatus).HasColumnName("ApprovalStatus");            
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.InvDamages)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.InvDamages1)
                .HasForeignKey(d => d.ModifiedBy);

            this.HasRequired(t => t.InvStore)
             .WithMany(t => t.InvDamages)
             .HasForeignKey(d => d.InvStoreId);
            this.HasOptional(t => t.SecCompany)
                .WithMany(t => t.InvDamages)
                .HasForeignKey(d => d.SecCompanyId);
          

        }
    }
}
