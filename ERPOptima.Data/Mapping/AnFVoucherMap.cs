using ERPOptima.Model.Accounts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class AnFVoucherMap : EntityTypeConfiguration<AnFVoucher>
    {
        public AnFVoucherMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VoucherNumber)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Naration)
                .HasMaxLength(256);

            this.Property(t => t.CancelReason)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("AnFVouchers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CmnCompanyId).HasColumnName("CmnCompanyId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.VoucherNumber).HasColumnName("VoucherNumber");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.SlsOfficeId).HasColumnName("SlsOfficeId");
            this.Property(t => t.AnFCostCenterId).HasColumnName("AnFCostCenterId");
            this.Property(t => t.CmnFinancialYearId).HasColumnName("CmnFinancialYearId");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.Naration).HasColumnName("Naration");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IsPosted).HasColumnName("IsPosted");
            this.Property(t => t.PostedBy).HasColumnName("PostedBy");
            this.Property(t => t.PostedDate).HasColumnName("PostedDate");
            this.Property(t => t.IsCancel).HasColumnName("IsCancel");
            this.Property(t => t.CancelledBy).HasColumnName("CancelledBy");
            this.Property(t => t.CancelledDate).HasColumnName("CancelledDate");
            this.Property(t => t.CancelReason).HasColumnName("CancelReason");
            this.Property(t => t.Purpose).HasColumnName("Purpose");
            this.Property(t => t.FileLocation).HasColumnName("FileLocation");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.CmnFinancialYear)
                .WithMany(t => t.AnFVouchers)
                .HasForeignKey(d => d.CmnFinancialYearId);

        }
    }
}
