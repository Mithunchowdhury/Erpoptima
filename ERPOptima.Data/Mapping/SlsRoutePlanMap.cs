using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsRoutePlanMap : EntityTypeConfiguration<SlsRoutePlan>
    {
        public SlsRoutePlanMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("SlsRoutePlans");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.WeekNo).HasColumnName("WeekNo");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ApprovalStatus).HasColumnName("ApprovalStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.HrmEmployee)
                .WithMany(t => t.SlsRoutePlans)
                .HasForeignKey(d => d.HrmEmployeeId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsRoutePlans)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsRoutePlans1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
