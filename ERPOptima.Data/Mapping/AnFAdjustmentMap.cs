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
    public class AnFAdjustmentMap : EntityTypeConfiguration<AnFAdjustment>
    {
        public AnFAdjustmentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AnFAdjustments");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AnFAdvanceId).HasColumnName("AnFAdvanceId");
            this.Property(t => t.AdjustedAmount).HasColumnName("AdjustedAmount");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.CmnFinancialYearId).HasColumnName("CmnFinancialYearId");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.AnFVoucherId).HasColumnName("AnFVoucherId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.AnFAdvance)
                .WithMany(t => t.AnFAdjustments)
                .HasForeignKey(d => d.AnFAdvanceId);
            this.HasOptional(t => t.AnFVoucher)
                .WithMany(t => t.AnFAdjustments)
                .HasForeignKey(d => d.AnFVoucherId);
            this.HasRequired(t => t.CmnFinancialYear)
                .WithMany(t => t.AnFAdjustments)
                .HasForeignKey(d => d.CmnFinancialYearId);
            this.HasOptional(t => t.SecCompany)
                .WithMany(t => t.AnFAdjustments)
                .HasForeignKey(d => d.SecCompanyId);

        }
    }
}
