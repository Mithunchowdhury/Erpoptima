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
    public class AnFClosingBalanceMap : EntityTypeConfiguration<AnFClosingBalance>
    {
        public AnFClosingBalanceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AnFClosingBalances");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CmnCompanyId).HasColumnName("CmnCompanyId");
            this.Property(t => t.AnfChartOfAccountId).HasColumnName("AnfChartOfAccountId");
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
            this.HasRequired(t => t.CmnFinancialYear)
                .WithMany(t => t.AnFClosingBalances)
                .HasForeignKey(d => d.CmnFinancialYearId);

        }
    }
}
