using ERPOptima.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class CmnApprovalUserPermissionMap : EntityTypeConfiguration<CmnApprovalUserPermission>
    {
        public CmnApprovalUserPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CmnApprovalUserPermissions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CmnApprovalProcessLevelId).HasColumnName("CmnApprovalProcessLevelId");
            this.Property(t => t.SecUserId).HasColumnName("SecUserId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.CmnApprovalProcessLevel)
                .WithMany(t => t.CmnApprovalUserPermissions)
                .HasForeignKey(d => d.CmnApprovalProcessLevelId);

        }
    }
}