using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvIssueDetailMap : EntityTypeConfiguration<InvIssueDetail>
    {
        public InvIssueDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("InvIssueDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvIssueId).HasColumnName("InvIssueId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.RequiredQuantity).HasColumnName("RequiredQuantity");
            this.Property(t => t.IssuedQuantity).HasColumnName("IssuedQuantity");
            this.Property(t => t.SlsUnitId).HasColumnName("SlsUnitId");
           

            // Relationships
            this.HasRequired(t => t.InvIssue)
                .WithMany(t => t.InvIssueDetails)
                .HasForeignKey(d => d.InvIssueId);
            this.HasRequired(t => t.SlsProduct)
                .WithMany(t => t.InvIssueDetails)
                .HasForeignKey(d => d.SlsProductId);
            this.HasRequired(t => t.SlsUnit)
                .WithMany(t => t.InvIssueDetails)
                .HasForeignKey(d => d.SlsUnitId);

        }
    }
}
