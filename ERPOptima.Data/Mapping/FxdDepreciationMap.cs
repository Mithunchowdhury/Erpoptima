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
    public class FxdDepreciationMap : EntityTypeConfiguration<FxdDepreciation>
    {
        public FxdDepreciationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Remarks)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("FxdDepreciations");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.FxdAcquisitionId).HasColumnName("FxdAcquisitionId");
            this.Property(t => t.CmnFinancialYearId).HasColumnName("CmnFinancialYearId");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.DepreciationRate).HasColumnName("DepreciationRate");
            this.Property(t => t.DepreciationMethod).HasColumnName("DepreciationMethod");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Depreciation).HasColumnName("Depreciation");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.WrittenDownValue).HasColumnName("WrittenDownValue");
            this.Property(t => t.AnfVoucherId).HasColumnName("AnfVoucherId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.AnFVoucher)
                .WithMany(t => t.FxdDepreciations)
                .HasForeignKey(d => d.AnfVoucherId);
            this.HasRequired(t => t.CmnFinancialYear)
                .WithMany(t => t.FxdDepreciations)
                .HasForeignKey(d => d.CmnFinancialYearId);
            this.HasOptional(t => t.FxdAcquisition)
                .WithMany(t => t.FxdDepreciations)
                .HasForeignKey(d => d.FxdAcquisitionId);
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.FxdDepreciations)
                .HasForeignKey(d => d.SecCompanyId);

        }
    }
}
