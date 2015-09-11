using ERPOptima.Model.HRM;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class HrmDesignationMap : EntityTypeConfiguration<HrmDesignation>
    {
        public HrmDesignationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.ShortName)
                .IsRequired()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("HrmDesignations");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.HrmDesignationId).HasColumnName("HrmDesignationId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.HrmDesignation1)
                .WithMany(t => t.HrmDesignations1)
                .HasForeignKey(d => d.HrmDesignationId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.HrmDesignations)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.HrmDesignations1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
