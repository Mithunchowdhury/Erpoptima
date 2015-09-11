using ERPOptima.Model.Security;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SecRolePermissionMap : EntityTypeConfiguration<SecRolePermission>
    {
        public SecRolePermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SecRolePermissions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SecRoleId).HasColumnName("SecRoleId");
            this.Property(t => t.SecResourceId).HasColumnName("SecResourceId");
            this.Property(t => t.Add).HasColumnName("Add");
            this.Property(t => t.ReadOnly).HasColumnName("ReadOnly");
            this.Property(t => t.Edit).HasColumnName("Edit");
            this.Property(t => t.Delete).HasColumnName("Delete");
            this.Property(t => t.Print).HasColumnName("Print");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecResource)
                .WithMany(t => t.SecRolePermissions)
                .HasForeignKey(d => d.SecResourceId);
            this.HasRequired(t => t.SecRole)
                .WithMany(t => t.SecRolePermissions)
                .HasForeignKey(d => d.SecRoleId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SecRolePermissions)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SecRolePermissions1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
