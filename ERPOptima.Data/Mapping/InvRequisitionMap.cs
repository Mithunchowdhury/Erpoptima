using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvRequisitionMap : EntityTypeConfiguration<InvRequisition>
    {
        public InvRequisitionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RequisitionCode)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Remarks)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("InvRequisitions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RequisitionCode).HasColumnName("RequisitionCode");
            this.Property(t => t.PreferredDeliveryDate).HasColumnName("PreferredDeliveryDate");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ApprovalStatus).HasColumnName("ApprovalStatus");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.SecCompany)
                .WithMany(t => t.InvRequisitions)
                .HasForeignKey(d => d.SecCompanyId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.InvRequisitions)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.InvRequisitions1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
