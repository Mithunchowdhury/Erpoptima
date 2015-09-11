using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Mapping
{
    public class FxdRevaluationMap : EntityTypeConfiguration<FxdRevaluation>
    {
        public FxdRevaluationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Remarks)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("FxdRevaluations");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.FxdAcquisitionId).HasColumnName("FxdAcquisitionId");
            this.Property(t => t.Presentvalue).HasColumnName("Presentvalue");
            this.Property(t => t.Revalue).HasColumnName("Revalue");
            this.Property(t => t.AnfVoucherId).HasColumnName("AnfVoucherId");
            this.Property(t => t.CmnFinancialYearId).HasColumnName("CmnFinancialYearId");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.AnFVoucher)
                .WithMany(t => t.FxdRevaluations)
                .HasForeignKey(d => d.AnfVoucherId);
            this.HasRequired(t => t.CmnFinancialYear)
                .WithMany(t => t.FxdRevaluations)
                .HasForeignKey(d => d.CmnFinancialYearId);
            this.HasRequired(t => t.FxdAcquisition)
                .WithMany(t => t.FxdRevaluations)
                .HasForeignKey(d => d.FxdAcquisitionId);
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.FxdRevaluations)
                .HasForeignKey(d => d.SecCompanyId);

        }
    }
}
