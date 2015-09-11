using ERPOptima.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class CmnApprovalMap : EntityTypeConfiguration<CmnApproval>
    {
        public CmnApprovalMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CmnApprovals");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CmnApprovalProcessId).HasColumnName("CmnApprovalProcessId");
            this.Property(t => t.RefId).HasColumnName("RefId");
            this.Property(t => t.CmnProcessLevelId).HasColumnName("CmnProcessLevelId");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.DoneBy).HasColumnName("DoneBy");
            this.Property(t => t.DoneDateTime).HasColumnName("DoneDateTime");
            this.Property(t => t.CmnCompanyId).HasColumnName("CmnCompanyId");

            // Relationships
            this.HasRequired(t => t.CmnApprovalProcess)
                .WithMany(t => t.CmnApprovals)
                .HasForeignKey(d => d.CmnApprovalProcessId);
            this.HasRequired(t => t.CmnProcessLevel)
                .WithMany(t => t.CmnApprovals)
                .HasForeignKey(d => d.CmnProcessLevelId);

        }
    }
}
