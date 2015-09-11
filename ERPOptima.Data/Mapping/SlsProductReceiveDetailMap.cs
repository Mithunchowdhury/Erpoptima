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
    public class SlsProductReceiveDetailMap : EntityTypeConfiguration<SlsProductReceiveDetail>
    {
        public SlsProductReceiveDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsProductReceiveDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsProductReceiveId).HasColumnName("SlsProductReceiveId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
            this.Property(t => t.DeliveryQuantity).HasColumnName("DeliveryQuantity");
            this.Property(t => t.ReceivedQuantity).HasColumnName("ReceivedQuantity");
            this.Property(t => t.Remarks).HasColumnName("Remarks");            

            // Relationships
            this.HasRequired(t => t.SlsProductReceive)
                .WithMany(t => t.SlsProductReceiveDetails)
                .HasForeignKey(d => d.SlsProductReceiveId);
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.SlsProductReceiveDetails)
                .HasForeignKey(d => d.SlsProductId);
            this.HasRequired(t => t.SlsUnit)
                .WithMany(t => t.SlsProductReceiveDetails)
                .HasForeignKey(d => d.SlsUnitId);
        }
    }
}
