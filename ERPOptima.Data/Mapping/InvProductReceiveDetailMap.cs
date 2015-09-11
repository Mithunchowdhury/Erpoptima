using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvProductReceiveDetailMap : EntityTypeConfiguration<InvProductReceiveDetail>
    {
        public InvProductReceiveDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("InvProductReceiveDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvProductReceiveId).HasColumnName("InvProductReceiveId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.IssuedQuantity).HasColumnName("IssuedQuantity");
            this.Property(t => t.ReceivedQuantity).HasColumnName("ReceivedQuantity");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");

            // Relationships
            this.HasOptional(t => t.InvProductReceive)
                .WithMany(t => t.InvProductReceiveDetails)
                .HasForeignKey(d => d.InvProductReceiveId);
            this.HasOptional(t => t.SlsUnit)
                .WithMany(t => t.InvProductReceiveDetails)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
