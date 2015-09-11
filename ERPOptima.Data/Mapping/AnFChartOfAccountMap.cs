using ERPOptima.Model.Accounts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class AnFChartOfAccountMap : EntityTypeConfiguration<AnFChartOfAccount>
    {
        public AnFChartOfAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.ScheduleNo)
                .HasMaxLength(16);

            // Table & Column Mappings
            this.ToTable("AnFChartOfAccounts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AnFChartOfAccountId).HasColumnName("AnFChartOfAccountId");
            this.Property(t => t.CmnCompanyId).HasColumnName("CmnCompanyId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ScheduleNo).HasColumnName("ScheduleNo");
            this.Property(t => t.IsTransactionalHead).HasColumnName("IsTransactionalHead");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.DepthLevel).HasColumnName("DepthLevel");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.AnFChartOfAccount1)
                .WithMany(t => t.AnFChartOfAccounts1)
                .HasForeignKey(d => d.AnFChartOfAccountId);

        }
    }
}
