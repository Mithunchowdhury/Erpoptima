using ERPOptima.Model.Accounts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class AnFOpeningBalanceMap : EntityTypeConfiguration<AnFOpeningBalance>
    {
        public AnFOpeningBalanceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AnFOpeningBalances");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CmnCompanyId).HasColumnName("CmnCompanyId");
            //this.Property(t => t.CmnProjectId).HasColumnName("CmnProjectId");
            this.Property(t => t.AnFChartOfAccountId).HasColumnName("AnFChartOfAccountId");
            this.Property(t => t.Debit).HasColumnName("Debit");
            this.Property(t => t.Credit).HasColumnName("Credit");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CmnFinancialYearId).HasColumnName("CmnFinancialYearId");
            this.Property(t => t.IsEditable).HasColumnName("IsEditable");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.AnFChartOfAccount)
                .WithMany(t => t.AnFOpeningBalances)
                .HasForeignKey(d => d.AnFChartOfAccountId);
            this.HasRequired(t => t.CmnFinancialYear)
                .WithMany(t => t.AnFOpeningBalances)
                .HasForeignKey(d => d.CmnFinancialYearId);
            //this.HasRequired(t => t.CmnProject)
            //    .WithMany(t => t.AnFOpeningBalances)
            //    .HasForeignKey(d => d.CmnProjectId);

        }
    }
}
