using ERPOptima.Model.Accounts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class AnFMonthLockMap : EntityTypeConfiguration<AnFMonthLock>
    {
        public AnFMonthLockMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AnFMonthLocks");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CmnFinancialYearId).HasColumnName("CmnFinancialYearId");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.MonthNo).HasColumnName("MonthNo");
            this.Property(t => t.LockingStatus).HasColumnName("LockingStatus");
            this.Property(t => t.SecConpanyId).HasColumnName("SecConpanyId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.CmnFinancialYear)
                .WithMany(t => t.AnFMonthLocks)
                .HasForeignKey(d => d.CmnFinancialYearId);
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.AnFMonthLocks)
                .HasForeignKey(d => d.SecConpanyId);

        }
    }
}
