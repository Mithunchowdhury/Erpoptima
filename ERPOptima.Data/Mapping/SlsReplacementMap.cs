using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsReplacementMap : EntityTypeConfiguration<SlsReplacement>
    {
        public SlsReplacementMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("SlsReplacements");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.SlsSalesOrderId).HasColumnName("SlsSalesOrderId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
