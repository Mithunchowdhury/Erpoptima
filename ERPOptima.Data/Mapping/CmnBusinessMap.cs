using ERPOptima.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class CmnBusinessMap : EntityTypeConfiguration<CmnBusiness>
    {
        public CmnBusinessMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Type)
                .HasMaxLength(64);

            this.Property(t => t.Remarks)
                .HasMaxLength(128);

            this.Property(t => t.Prefix)
                .IsRequired()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("CmnBusinesses");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CmnCompanyId).HasColumnName("CmnCompanyId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Prefix).HasColumnName("Prefix");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
