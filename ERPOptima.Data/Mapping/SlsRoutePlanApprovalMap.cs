using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsRoutePlanApprovalMap : EntityTypeConfiguration<SlsRoutePlanApproval>
    {
        public SlsRoutePlanApprovalMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Comment)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SlsRoutePlanApprovals");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsRoutePlanId).HasColumnName("SlsRoutePlanId");
            this.Property(t => t.From).HasColumnName("From");
            this.Property(t => t.To).HasColumnName("To");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SlsRoutePlan)
                .WithMany(t => t.SlsRoutePlanApprovals)
                .HasForeignKey(d => d.SlsRoutePlanId);

        }
    }
}
