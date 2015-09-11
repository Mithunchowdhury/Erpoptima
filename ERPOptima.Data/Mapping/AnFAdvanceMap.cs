using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPOptima.Model.Accounts;

namespace ERPOptima.Data.Mapping
{
    public class AnFAdvanceMap : EntityTypeConfiguration<AnFAdvance>
    {
        public AnFAdvanceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Purpose)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("AnFAdvances");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.CmnFinancialYearId).HasColumnName("CmnFinancialYearId");
            this.Property(t => t.AnFVoucherId).HasColumnName("AnFVoucherId");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.AnFCostCenterId).HasColumnName("AnFCostCenterId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Advance).HasColumnName("Advance");
            this.Property(t => t.Purpose).HasColumnName("Purpose");
            this.Property(t => t.ProposedReturnDate).HasColumnName("ProposedReturnDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.FIleLocation).HasColumnName("FIleLocation");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.AnFCostCenter)
                .WithMany(t => t.AnFAdvances)
                .HasForeignKey(d => d.AnFCostCenterId);
            this.HasOptional(t => t.AnFVoucher)
                .WithMany(t => t.AnFAdvances)
                .HasForeignKey(d => d.AnFVoucherId);
            this.HasRequired(t => t.CmnFinancialYear)
                .WithMany(t => t.AnFAdvances)
                .HasForeignKey(d => d.CmnFinancialYearId);
            this.HasRequired(t => t.HrmEmployee)
                .WithMany(t => t.AnFAdvances)
                .HasForeignKey(d => d.HrmEmployeeId);
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.AnFAdvances)
                .HasForeignKey(d => d.SecCompanyId);

        }
    }
}
