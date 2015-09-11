using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsCorporateSalesApplicationDetailMap : EntityTypeConfiguration<SlsCorporateSalesApplicationDetail>
    {
        public SlsCorporateSalesApplicationDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsCorporateSalesApplicationDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsCorporateSalesApplicationId).HasColumnName("SlsCorporateSalesApplicationId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.AppliedPercentage).HasColumnName("AppliedPercentage");
            this.Property(t => t.ApprovedPercentage).HasColumnName("ApprovedPercentage");

            // Relationships
            this.HasRequired(t => t.SlsCorporateSalesApplication)
                .WithMany(t => t.SlsCorporateSalesApplicationDetails)
                .HasForeignKey(d => d.SlsCorporateSalesApplicationId).WillCascadeOnDelete(false);
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.SlsCorporateSalesApplicationDetails)
                .HasForeignKey(d => d.SlsProductId);

        }
    }
}
