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
    public class FxdAcquisitionMap : EntityTypeConfiguration<FxdAcquisition>
    {
        public FxdAcquisitionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Name)
                .HasMaxLength(512);

            this.Property(t => t.Supplier)
                .HasMaxLength(128);

            this.Property(t => t.Manufacturer)
                .HasMaxLength(128);

            this.Property(t => t.ModelOrBatchNo)
                .HasMaxLength(128);

            this.Property(t => t.SerialNo)
                .HasMaxLength(128);

            this.Property(t => t.Description)
                .HasMaxLength(256);

            this.Property(t => t.Location)
                .HasMaxLength(256);

            this.Property(t => t.Remarks)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("FxdAcquisitions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FxdAssetId).HasColumnName("FxdAssetId");
            this.Property(t => t.CmnFinancialYearId).HasColumnName("CmnFinancialYearId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Supplier).HasColumnName("Supplier");
            this.Property(t => t.Manufacturer).HasColumnName("Manufacturer");
            this.Property(t => t.ModelOrBatchNo).HasColumnName("ModelOrBatchNo");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
            this.Property(t => t.WarrantyExpreDate).HasColumnName("WarrantyExpreDate");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.DepreciationRate).HasColumnName("DepreciationRate");
            this.Property(t => t.DepreciationMethod).HasColumnName("DepreciationMethod");
            this.Property(t => t.DrepreciationStartDate).HasColumnName("DrepreciationStartDate");
            this.Property(t => t.DepresiationEndDate).HasColumnName("DepresiationEndDate");
            this.Property(t => t.AcquisitionCost).HasColumnName("AcquisitionCost");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.TotalAcquisitionCost).HasColumnName("TotalAcquisitionCost");
            this.Property(t => t.AnfVoucherId).HasColumnName("AnfVoucherId");
            this.Property(t => t.LifeTime).HasColumnName("LifeTime");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.FileLocation).HasColumnName("FileLocation");
            
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.AnFVoucher)
                .WithMany(t => t.FxdAcquisitions)
                .HasForeignKey(d => d.AnfVoucherId);
            this.HasRequired(t => t.CmnFinancialYear)
                .WithMany(t => t.FxdAcquisitions)
                .HasForeignKey(d => d.CmnFinancialYearId);
            this.HasRequired(t => t.FxdAsset)
                .WithMany(t => t.FxdAcquisitions)
                .HasForeignKey(d => d.FxdAssetId);
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.FxdAcquisitions)
                .HasForeignKey(d => d.SecCompanyId);

        }
    }
}
