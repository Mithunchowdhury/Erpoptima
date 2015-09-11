using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvDamageApprovalMap : EntityTypeConfiguration<InvDamageApproval>
    {
        public InvDamageApprovalMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Comment)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("InvDamageApprovals");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvDamageId).HasColumnName("InvDamageId");
            this.Property(t => t.From).HasColumnName("From");
            this.Property(t => t.To).HasColumnName("To");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.InvDamage)
                .WithMany(t => t.InvDamageApprovals)
                .HasForeignKey(d => d.InvDamageId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.InvDamageApprovals)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.InvDamageApprovals1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
