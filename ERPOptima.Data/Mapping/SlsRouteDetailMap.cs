using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsRouteDetailMap : EntityTypeConfiguration<SlsRouteDetail>
    {
        public SlsRouteDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsRouteDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsRouteId).HasColumnName("SlsRouteId");
            this.Property(t => t.PartyType).HasColumnName("PartyType");
            this.Property(t => t.SlsDistributorId).HasColumnName("SlsDistributorId");
            this.Property(t => t.SlsRetailerId).HasColumnName("SlsRetailerId");
            this.Property(t => t.SlsCorporateClientId).HasColumnName("SlsCorporateClientId");
            this.Property(t => t.SlsDealerId).HasColumnName("SlsDealerId");

            // Relationships
            this.HasOptional(t => t.SlsCorporateClient)
                .WithMany(t => t.SlsRouteDetails)
                .HasForeignKey(d => d.SlsCorporateClientId);
            this.HasOptional(t => t.SlsDealer)
                .WithMany(t => t.SlsRouteDetails)
                .HasForeignKey(d => d.SlsDealerId);
            this.HasOptional(t => t.SlsDistributor)
                .WithMany(t => t.SlsRouteDetails)
                .HasForeignKey(d => d.SlsDistributorId);
            this.HasOptional(t => t.SlsRetailer)
                .WithMany(t => t.SlsRouteDetails)
                .HasForeignKey(d => d.SlsRetailerId);
            this.HasRequired(t => t.SlsRoute)
                .WithMany(t => t.SlsRouteDetails)
                .HasForeignKey(d => d.SlsRouteId);

        }
    }
}
