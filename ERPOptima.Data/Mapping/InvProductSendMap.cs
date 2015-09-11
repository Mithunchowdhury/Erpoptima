using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvProductSendMap : EntityTypeConfiguration<InvProductSend>
    {
        public InvProductSendMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.GatePassNo)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.VehicleNo)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("InvProductSends");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.GatePassNo).HasColumnName("GatePassNo");
            this.Property(t => t.VehicleNo).HasColumnName("VehicleNo");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.InvProductSends)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.InvProductSends1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
