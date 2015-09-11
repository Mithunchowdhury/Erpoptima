using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsAreaConfigurationMap : EntityTypeConfiguration<SlsAreaConfiguration>
    {
        public SlsAreaConfigurationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Remarks)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SlsAreaConfigurations");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.IsRegionBased).HasColumnName("IsRegionBased");
            this.Property(t => t.IsOfficeBased).HasColumnName("IsOfficeBased");
            this.Property(t => t.IsDistrictBased).HasColumnName("IsDistrictBased");
            this.Property(t => t.IsThanaBased).HasColumnName("IsThanaBased");
            this.Property(t => t.IsAreaBased).HasColumnName("IsAreaBased");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.HrmEmployee)
                .WithMany(t => t.SlsAreaConfigurations)
                .HasForeignKey(d => d.HrmEmployeeId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsAreaConfigurations)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsAreaConfigurations1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
