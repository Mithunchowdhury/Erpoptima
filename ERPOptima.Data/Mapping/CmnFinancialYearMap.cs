using ERPOptima.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class CmnFinancialYearMap : EntityTypeConfiguration<CmnFinancialYear>
    {
        public CmnFinancialYearMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ClosingNote)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("CmnFinancialYears");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.OpeningDate).HasColumnName("OpeningDate");
            this.Property(t => t.ClosingDate).HasColumnName("ClosingDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CmnCompanyId).HasColumnName("CmnCompanyId");
            this.Property(t => t.SecModuleId).HasColumnName("SecModuleId");
            this.Property(t => t.YearClosingStatus).HasColumnName("YearClosingStatus");
            this.Property(t => t.ClosedBy).HasColumnName("ClosedBy");
            this.Property(t => t.ClosedDate).HasColumnName("ClosedDate");
            this.Property(t => t.ClosingNote).HasColumnName("ClosingNote");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecModule)
                .WithMany(t => t.CmnFinancialYears)
                .HasForeignKey(d => d.SecModuleId);

        }
    }
}
