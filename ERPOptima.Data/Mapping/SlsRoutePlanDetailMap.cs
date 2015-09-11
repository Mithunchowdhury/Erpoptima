using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsRoutePlanDetailMap : EntityTypeConfiguration<SlsRoutePlanDetail>
    {
        public SlsRoutePlanDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsRoutePlanDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsRoutePlanId).HasColumnName("SlsRoutePlanId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.SlsRouteId).HasColumnName("SlsRouteId");

            // Relationships
            this.HasRequired(t => t.SlsRoutePlan)
                .WithMany(t => t.SlsRoutePlanDetails)
                .HasForeignKey(d => d.SlsRoutePlanId);
            this.HasOptional(t => t.SlsRoute)
                .WithMany(t => t.SlsRoutePlanDetails)
                .HasForeignKey(d => d.SlsRouteId);

        }
    }
}
