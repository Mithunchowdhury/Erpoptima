using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsThanaMap : EntityTypeConfiguration<SlsThana>
    {
        public SlsThanaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("SlsThanas");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsDistrictId).HasColumnName("SlsDistrictId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsThanas)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsThanas1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SlsDistrict)
                .WithMany(t => t.SlsThanas)
                .HasForeignKey(d => d.SlsDistrictId);

        }
    }
}
