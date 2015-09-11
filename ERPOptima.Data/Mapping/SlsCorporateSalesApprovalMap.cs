using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsCorporateSalesApprovalMap : EntityTypeConfiguration<SlsCorporateSalesApproval>
    {
        public SlsCorporateSalesApprovalMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Comment)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SlsCorporateSalesApprovals");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsCorporateSalesApplicationId).HasColumnName("SlsCorporateSalesApplicationId");
            this.Property(t => t.From).HasColumnName("From");
            this.Property(t => t.To).HasColumnName("To");
            this.Property(t => t.Event).HasColumnName("Event");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsCorporateSalesApprovals)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsCorporateSalesApprovals1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SlsCorporateSalesApplication)
                .WithMany(t => t.SlsCorporateSalesApprovals)
                .HasForeignKey(d => d.SlsCorporateSalesApplicationId).WillCascadeOnDelete(false);

        }
    }
}
