using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsSalesTargetMap : EntityTypeConfiguration<SlsSalesTarget>
    {
        public SlsSalesTargetMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("SlsSalesTargets");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.TargetSalesAmount).HasColumnName("TargetSalesAmount");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.HrmEmployee)
                .WithMany(t => t.SlsSalesTargets)
                .HasForeignKey(d => d.HrmEmployeeId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsSalesTargets)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsSalesTargets1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.SlsSalesTargets)
                .HasForeignKey(d => d.SecCompanyId);
        }
    }
}
