using ERPOptima.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class CmnApprovalProcessMap : EntityTypeConfiguration<CmnApprovalProcess>
    {
        public CmnApprovalProcessMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.ShortName)
                .HasMaxLength(32);

            this.Property(t => t.Url)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("CmnApprovalProcesses");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.SecModuleId).HasColumnName("SecModuleId");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.SecModule)
                .WithMany(t => t.CmnApprovalProcesses)
                .HasForeignKey(d => d.SecModuleId);

        }
    }
}
