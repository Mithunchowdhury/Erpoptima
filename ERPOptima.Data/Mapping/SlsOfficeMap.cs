using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsOfficeMap : EntityTypeConfiguration<SlsOffice>
    {
        public SlsOfficeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Code)
                .HasMaxLength(3);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.Phone)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("SlsOffices");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SlsRegionId).HasColumnName("SlsRegionId");
            this.Property(t => t.SlsOfficeTypeId).HasColumnName("SlsOfficeTypeId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Head).HasColumnName("Head");
            this.Property(t => t.InCharge).HasColumnName("InCharge");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.HrmEmployee)
                .WithMany(t => t.SlsOffices)
                .HasForeignKey(d => d.InCharge);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsOffices)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsOffices1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SlsOffice1)
                .WithMany(t => t.SlsOffices1)
                .HasForeignKey(d => d.Head);
            this.HasOptional(t => t.SlsOfficeType)
                .WithMany(t => t.SlsOffices)
                .HasForeignKey(d => d.SlsOfficeTypeId);
            this.HasRequired(t => t.SlsRegion)
                .WithMany(t => t.SlsOffices)
                .HasForeignKey(d => d.SlsRegionId);

        }
    }
}
