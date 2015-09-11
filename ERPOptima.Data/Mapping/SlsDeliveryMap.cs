using ERPOptima.Model.Sales;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;


namespace ERPOptima.Data.Mapping
{
    public class SlsDeliveryMap : EntityTypeConfiguration<SlsDelivery>
    {
        public SlsDeliveryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChallanNo)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.InvoiceNo)
                .HasMaxLength(256);

            this.Property(t => t.VehicleNo)
                .HasMaxLength(256);

            this.Property(t => t.Remarks)
                .HasMaxLength(512);
            this.Property(t => t.ReceivedRemarks)
               .HasMaxLength(512);
            // Table & Column Mappings
            this.ToTable("SlsDeliveries");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsSalesOrderId).HasColumnName("SlsSalesOrderId");
            this.Property(t => t.DeliveryDate).HasColumnName("DeliveryDate");
            this.Property(t => t.ChallanNo).HasColumnName("ChallanNo");
            this.Property(t => t.InvoiceNo).HasColumnName("InvoiceNo");
            this.Property(t => t.VehicleNo).HasColumnName("VehicleNo");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.Total).HasColumnName("Total");
            this.Property(t => t.ReceivedStatus).HasColumnName("ReceivedStatus");
            this.Property(t => t.ReceivedDate).HasColumnName("ReceivedDate");
            this.Property(t => t.ReceivedRemarks).HasColumnName("ReceivedRemarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.SlsSalesOrder)
                .WithMany(t => t.SlsDeliveries)
                .HasForeignKey(d => d.SlsSalesOrderId);
            this.Property(t => t.ChallanNo).IsRequired().HasMaxLength(256).HasColumnAnnotation(IndexAnnotation.AnnotationName,new IndexAnnotation(
                new IndexAttribute("IX_SlsDeliveries", 1) { IsUnique = true }));

        }
    }
}
