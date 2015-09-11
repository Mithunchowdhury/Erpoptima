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
    public class SlsProductReceiveMap : EntityTypeConfiguration<SlsProductReceive>
    {
        public SlsProductReceiveMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChallanNo)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("SlsProductReceives");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsDeliveryId).HasColumnName("SlsDeliveryId");
            this.Property(t => t.ChallanNo).HasColumnName("ChallanNo");
            this.Property(t => t.InvoiceNo).HasColumnName("InvoiceNo");
            this.Property(t => t.VehicleNo).HasColumnName("VehicleNo");
            this.Property(t => t.RcvDate).HasColumnName("RcvDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SlsDelivery)
                .WithMany(t => t.SlsProductReceives)
                .HasForeignKey(d => d.SlsDeliveryId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsProductReceives)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsProductReceives1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
