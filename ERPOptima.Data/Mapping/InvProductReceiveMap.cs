using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvProductReceiveMap : EntityTypeConfiguration<InvProductReceive>
    {
        public InvProductReceiveMap()
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
            this.ToTable("InvProductReceives");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvStoreId).HasColumnName("InvStoreId");
            this.Property(t => t.InvIssueId).HasColumnName("InvIssueId");
            this.Property(t => t.ChallanNo).HasColumnName("ChallanNo");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.InvIssue)
                .WithMany(t => t.InvProductReceives)
                .HasForeignKey(d => d.InvIssueId);
            this.HasRequired(t => t.InvStore)
                .WithMany(t => t.InvProductReceives)
                .HasForeignKey(d => d.InvStoreId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.InvProductReceives)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.InvProductReceives1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
