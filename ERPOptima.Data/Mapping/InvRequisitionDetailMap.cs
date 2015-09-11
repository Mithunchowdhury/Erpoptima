using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvRequisitionDetailMap : EntityTypeConfiguration<InvRequisitionDetail>
    {
        public InvRequisitionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("InvRequisitionDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvRequisitionId).HasColumnName("InvRequisitionId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.RequiredQuantity).HasColumnName("RequiredQuantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");

            // Relationships
            this.HasRequired(t => t.InvRequisition)
                .WithMany(t => t.InvRequisitionDetails)
                .HasForeignKey(d => d.InvRequisitionId);
            this.HasRequired(t => t.SlsProduct)
               .WithMany(t => t.InvRequisitionDetails)
               .HasForeignKey(d => d.SlsProductId);
            this.HasRequired(t => t.SlsUnit)
                .WithMany(t => t.InvRequisitionDetails)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
