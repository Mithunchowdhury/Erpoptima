using ERPOptima.Model.Security;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SecResourceMap : EntityTypeConfiguration<SecResource>
    {
        public SecResourceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.DisplayName)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Tag)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("SecResources");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.SecResourcesId).HasColumnName("SecResourcesId");
            this.Property(t => t.SecModuleId).HasColumnName("SecModuleId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecModule)
                .WithMany(t => t.SecResources)
                .HasForeignKey(d => d.SecModuleId);
            this.HasOptional(t => t.SecResource1)
                .WithMany(t => t.SecResources1)
                .HasForeignKey(d => d.SecResourcesId);
            this.HasOptional(t => t.SecUser)
                .WithMany(t => t.SecResources)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
