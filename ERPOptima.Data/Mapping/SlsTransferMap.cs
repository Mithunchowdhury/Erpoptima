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
    public class SlsTransferMap : EntityTypeConfiguration<SlsTransfer>
    {
        public SlsTransferMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VehicleNo)
                .HasMaxLength(64);

            this.Property(t => t.ChallenNo)
                .HasMaxLength(64);

            this.Property(t => t.GatepassNo)
                .HasMaxLength(64);

            this.Property(t => t.Remarks)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("SlsTransfers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.From).HasColumnName("From");
            this.Property(t => t.FromInvStoreId).HasColumnName("FromInvStoreId");
            this.Property(t => t.To).HasColumnName("To");
            this.Property(t => t.ToInvStoreId).HasColumnName("ToInvStoreId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.VehicleNo).HasColumnName("VehicleNo");
            this.Property(t => t.ChallenNo).HasColumnName("ChallenNo");
            this.Property(t => t.GatepassNo).HasColumnName("GatepassNo");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.InvStore)
                .WithMany(t => t.SlsTransfers)
                .HasForeignKey(d => d.FromInvStoreId);
            this.HasRequired(t => t.InvStore1)
                .WithMany(t => t.SlsTransfers1)
                .HasForeignKey(d => d.ToInvStoreId).WillCascadeOnDelete(false);
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.SlsTransfers)
                .HasForeignKey(d => d.SecCompanyId);
            this.HasRequired(t => t.SlsOffice)
                .WithMany(t => t.SlsTransfers)
                .HasForeignKey(d => d.From);
            this.HasRequired(t => t.SlsOffice1)
                .WithMany(t => t.SlsTransfers1)
                .HasForeignKey(d => d.To).WillCascadeOnDelete(false);

        }
    }
}
