using ERPOptima.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class CmnApprovalProcessLevelMap : EntityTypeConfiguration<CmnApprovalProcessLevel>
    {
        public CmnApprovalProcessLevelMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CmnApprovalProcessLevels");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CmnCompanyId).HasColumnName("CmnCompanyId");
            this.Property(t => t.CmnApprovalProcessId).HasColumnName("CmnApprovalProcessId");
            this.Property(t => t.CmnProcessLevelId).HasColumnName("CmnProcessLevelId");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.CmnApprovalProcess)
                .WithMany(t => t.CmnApprovalProcessLevels)
                .HasForeignKey(d => d.CmnApprovalProcessId);
            this.HasRequired(t => t.CmnProcessLevel)
                .WithMany(t => t.CmnApprovalProcessLevels)
                .HasForeignKey(d => d.CmnProcessLevelId);

        }
    }
}
