using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsProductMap : EntityTypeConfiguration<SlsProduct>
    {
        public SlsProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Description)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("SlsProducts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.IsProduct).HasColumnName("IsProduct");
            this.Property(t => t.NoCredit).HasColumnName("NoCredit");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.SecCompany)
                .WithMany(t => t.SlsProducts)
                .HasForeignKey(d => d.SecCompanyId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsProducts)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsProducts1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SlsProduct1)
                .WithMany(t => t.SlsProducts1)
                .HasForeignKey(d => d.SlsProductId);

        }
    }
}
