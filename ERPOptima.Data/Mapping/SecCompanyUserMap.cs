using ERPOptima.Model.Security;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SecCompanyUserMap : EntityTypeConfiguration<SecCompanyUser>
    {
        public SecCompanyUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SecCompanyUsers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.SecUserId).HasColumnName("SecUserId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.SecCompanyUsers)
                .HasForeignKey(d => d.SecCompanyId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SecCompanyUsers)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SecCompanyUsers1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SecUser2)
                .WithMany(t => t.SecCompanyUsers2)
                .HasForeignKey(d => d.SecUserId).WillCascadeOnDelete(false);

        }
    }
}
