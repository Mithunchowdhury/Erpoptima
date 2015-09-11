using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsIncentiveSettingMap : EntityTypeConfiguration<SlsIncentiveSetting>
    {
        public SlsIncentiveSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Remarks)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("SlsIncentiveSettings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LowerLimit).HasColumnName("LowerLimit");
            this.Property(t => t.UpperLimit).IsOptional().HasColumnName("UpperLimit");
            this.Property(t => t.CommissionPercentage).HasColumnName("CommissionPercentage");
            this.Property(t => t.Remarks).IsOptional().HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsIncentiveSettings)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsIncentiveSettings1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
