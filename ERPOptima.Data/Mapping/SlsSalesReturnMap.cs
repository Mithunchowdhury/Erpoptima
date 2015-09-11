using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsSalesReturnMap : EntityTypeConfiguration<SlsSalesReturn>
    {
        public SlsSalesReturnMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("SlsSalesReturns");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PartyType).HasColumnName("PartyType");
            this.Property(t => t.Party).HasColumnName("Party");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.AdjustedAmount).HasColumnName("AdjustedAmount");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.SlsSalesReturns)
                .HasForeignKey(d => d.SecCompanyId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsSalesReturns)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsSalesReturns1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
