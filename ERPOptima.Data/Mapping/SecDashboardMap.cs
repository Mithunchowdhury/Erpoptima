using ERPOptima.Model.Security;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SecDashboardMap : EntityTypeConfiguration<SecDashboard>
    {
        public SecDashboardMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("SecDashboards");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.SecModuleId).HasColumnName("SecModuleId");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.SecDashboards)
                .HasForeignKey(d => d.SecCompanyId);
            this.HasRequired(t => t.SecModule)
                .WithMany(t => t.SecDashboards)
                .HasForeignKey(d => d.SecModuleId);

        }
    }
}
