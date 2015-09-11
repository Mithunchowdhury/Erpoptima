using ERPOptima.Model.Accounts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class AnFDepreciationRateMap : EntityTypeConfiguration<AnFDepreciationRate>
    {
        public AnFDepreciationRateMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Remarks)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("AnFDepreciationRates");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AnFChartOfAccountId).HasColumnName("AnFChartOfAccountId");
            this.Property(t => t.Rate).HasColumnName("Rate");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.AnFCOAParentId).HasColumnName("AnFCOAParentId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.AnFChartOfAccount)
                .WithMany(t => t.AnFDepreciationRates)
                .HasForeignKey(d => d.AnFChartOfAccountId);

        }
    }
}
