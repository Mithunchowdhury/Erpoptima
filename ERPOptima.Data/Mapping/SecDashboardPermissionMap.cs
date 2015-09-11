using ERPOptima.Model.Security;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SecDashboardPermissionMap : EntityTypeConfiguration<SecDashboardPermission>
    {
        public SecDashboardPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SecDashboardPermissions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SecRoleId).HasColumnName("SecRoleId");
            this.Property(t => t.SecDashboardId).HasColumnName("SecDashboardId");
            this.Property(t => t.IsPermitted).HasColumnName("IsPermitted");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecDashboard)
                .WithMany(t => t.SecDashboardPermissions)
                .HasForeignKey(d => d.SecDashboardId);
            this.HasRequired(t => t.SecRole)
                .WithMany(t => t.SecDashboardPermissions)
                .HasForeignKey(d => d.SecRoleId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SecDashboardPermissions)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SecDashboardPermissions1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
