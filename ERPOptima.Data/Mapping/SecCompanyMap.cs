using ERPOptima.Model.Security;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SecCompanyMap : EntityTypeConfiguration<SecCompany>
    {
        public SecCompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Address)
                .HasMaxLength(128);

            this.Property(t => t.ContactNo)
                .HasMaxLength(64);

            this.Property(t => t.Email)
                .HasMaxLength(64);

            this.Property(t => t.Web)
                .HasMaxLength(64);

            this.Property(t => t.Remarks)
                .HasMaxLength(256);

            this.Property(t => t.Prefix)
                .HasMaxLength(5);

            this.Property(t => t.ShortName)
                .HasMaxLength(4);

            // Table & Column Mappings
            this.ToTable("SecCompanies");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SecGroupId).HasColumnName("SecGroupId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.ContactNo).HasColumnName("ContactNo");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Web).HasColumnName("Web");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Prefix).HasColumnName("Prefix");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecGroup)
                .WithMany(t => t.SecCompanies)
                .HasForeignKey(d => d.SecGroupId);

        }
    }
}
