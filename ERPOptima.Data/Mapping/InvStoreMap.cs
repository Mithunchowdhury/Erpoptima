using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvStoreMap : EntityTypeConfiguration<InvStore>
    {
        public InvStoreMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Location)
                .HasMaxLength(128);

            this.Property(t => t.Remarks)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("InvStores");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsOfficeId).HasColumnName("SlsOfficeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.SlsDistributorId).HasColumnName("SlsDistributorId");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.SlsDistributor)
                .WithMany(t => t.InvStores)
                .HasForeignKey(d => d.SlsDistributorId);
            this.HasOptional(t => t.SlsOffice)
                .WithMany(t => t.InvStores)
                .HasForeignKey(d => d.SlsOfficeId);

        }
    }
}
