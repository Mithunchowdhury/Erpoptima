using ERPOptima.Model.Security;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SecUserMap : EntityTypeConfiguration<SecUser>
    {
        public SecUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LoginName)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.Password)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SecUsers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LoginName).HasColumnName("LoginName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.SecRoleId).HasColumnName("SecRoleId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecRole)
                .WithMany(t => t.SecUsers)
                .HasForeignKey(d => d.SecRoleId);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SecUsers1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SecUser2)
                .WithMany(t => t.SecUsers11)
                .HasForeignKey(d => d.CreatedBy);

        }
    }
}