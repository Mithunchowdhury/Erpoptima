using ERPOptima.Model.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Mapping
{
    public class SlsTransferDetailMap : EntityTypeConfiguration<SlsTransferDetail>
    {
        public SlsTransferDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsTransferDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsTransferId).HasColumnName("SlsTransferId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");

            // Relationships
            this.HasOptional(t => t.SlsProduct)
                .WithMany(t => t.SlsTransferDetails)
                .HasForeignKey(d => d.SlsProductId);
            this.HasOptional(t => t.SlsTransfer)
                .WithMany(t => t.SlsTransferDetails)
                .HasForeignKey(d => d.SlsTransferId);
            this.HasOptional(t => t.SlsUnit)
                .WithMany(t => t.SlsTransferDetails)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
