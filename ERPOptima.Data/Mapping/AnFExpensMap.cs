using ERPOptima.Model.Accounts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class AnFExpensMap : EntityTypeConfiguration<AnFExpens>
    {
        public AnFExpensMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Particular)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.BillNo)
                .HasMaxLength(256);

            this.Property(t => t.Narration)
                .IsRequired();

            this.Property(t => t.CancelReason)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("AnFExpenses");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.Particular).HasColumnName("Particular");
            this.Property(t => t.AnFChartOfAccountId).HasColumnName("AnFChartOfAccountId");
            this.Property(t => t.AnFCostCenterId).HasColumnName("AnFCostCenterId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.BillNo).HasColumnName("BillNo");
            this.Property(t => t.Mode).HasColumnName("Mode");
            this.Property(t => t.Narration).HasColumnName("Narration");
            this.Property(t => t.FileLocation).HasColumnName("FileLocation");
            this.Property(t => t.CmnFinancialYearId).HasColumnName("CmnFinancialYearId");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.AnFVoucherId).HasColumnName("AnFVoucherId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.IsPosted).HasColumnName("IsPosted");
            this.Property(t => t.PostedBy).HasColumnName("PostedBy");
            this.Property(t => t.PostedDate).HasColumnName("PostedDate");
            this.Property(t => t.IsCancel).HasColumnName("IsCancel");
            this.Property(t => t.CancelledBy).HasColumnName("CancelledBy");
            this.Property(t => t.CancelledDate).HasColumnName("CancelledDate");
            this.Property(t => t.CancelReason).HasColumnName("CancelReason");
            this.Property(t => t.Purpose).HasColumnName("Purpose");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.AnFChartOfAccount)
                .WithMany(t => t.AnFExpenses)
                .HasForeignKey(d => d.AnFChartOfAccountId);
            this.HasOptional(t => t.AnFCostCenter)
                .WithMany(t => t.AnFExpenses)
                .HasForeignKey(d => d.AnFCostCenterId);
            this.HasOptional(t => t.AnFVoucher)
                .WithMany(t => t.AnFExpenses)
                .HasForeignKey(d => d.AnFVoucherId);
            this.HasRequired(t => t.CmnFinancialYear)
                .WithMany(t => t.AnFExpenses)
                .HasForeignKey(d => d.CmnFinancialYearId);
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.AnFExpenses)
                .HasForeignKey(d => d.SecCompanyId);

        }
    }
}
