using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvIssueMap : EntityTypeConfiguration<InvIssue>
    {
        public InvIssueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IssueCode)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("InvIssues");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IssueCode).HasColumnName("IssueCode");
            this.Property(t => t.InvRequisitionId).HasColumnName("InvRequisitionId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.InvStoreId).HasColumnName("InvStoreId");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships

            
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.InvIssues)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.InvIssues1)
                .HasForeignKey(d => d.ModifiedBy);



        }
    }
}
