using ERPOptima.Model.Security;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SecGroupMap : EntityTypeConfiguration<SecGroup>
    {
        public SecGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(256);

            this.Property(t => t.Address)
                .HasMaxLength(2048);

            this.Property(t => t.ContactNo)
                .HasMaxLength(256);

            this.Property(t => t.Email)
                .HasMaxLength(128);

            this.Property(t => t.Remarks)
                .HasMaxLength(256);

            this.Property(t => t.Web)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("SecGroups");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.ContactNo).HasColumnName("ContactNo");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Web).HasColumnName("Web");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
