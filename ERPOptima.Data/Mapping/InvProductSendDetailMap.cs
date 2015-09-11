using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvProductSendDetailMap : EntityTypeConfiguration<InvProductSendDetail>
    {
        public InvProductSendDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChallanNo)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("InvProductSendDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvProductSendsId).HasColumnName("InvProductSendsId");
            this.Property(t => t.InvRequisitionsId).HasColumnName("InvRequisitionsId");
            this.Property(t => t.InvIssuesId).HasColumnName("InvIssuesId");
            this.Property(t => t.SlsOfficesId).HasColumnName("SlsOfficesId");
            this.Property(t => t.ChallanNo).HasColumnName("ChallanNo");

            // Relationships
            this.HasRequired(t => t.InvIssue)
                .WithMany(t => t.InvProductSendDetails)
                .HasForeignKey(d => d.InvIssuesId);
            this.HasRequired(t => t.InvProductSend)
                .WithMany(t => t.InvProductSendDetails)
                .HasForeignKey(d => d.InvProductSendsId);
            this.HasRequired(t => t.InvRequisition)
                .WithMany(t => t.InvProductSendDetails)
                .HasForeignKey(d => d.InvRequisitionsId).WillCascadeOnDelete(false);
            this.HasRequired(t => t.SlsOffice)
                .WithMany(t => t.InvProductSendDetails)
                .HasForeignKey(d => d.SlsOfficesId);

        }
    }
}
