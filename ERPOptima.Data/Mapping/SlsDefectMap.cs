using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsDefectMap : EntityTypeConfiguration<SlsDefect>
    {
        public SlsDefectMap()
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
            this.ToTable("SlsDefects");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsDistributorId).HasColumnName("SlsDistributorId");
            this.Property(t => t.SlsDealerId).HasColumnName("SlsDealerId");
            this.Property(t => t.SlsRetailerId).HasColumnName("SlsRetailerId");
            this.Property(t => t.SlsCorporateClientId).HasColumnName("SlsCorporateClientId");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsDefects)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsDefects1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SlsCorporateClient)
                .WithMany(t => t.SlsDefects)
                .HasForeignKey(d => d.SlsCorporateClientId);
            this.HasOptional(t => t.SlsDealer)
                .WithMany(t => t.SlsDefects)
                .HasForeignKey(d => d.SlsDealerId);
            this.HasOptional(t => t.SlsDistributor)
                .WithMany(t => t.SlsDefects)
                .HasForeignKey(d => d.SlsDistributorId);
            this.HasOptional(t => t.SlsRetailer)
                .WithMany(t => t.SlsDefects)
                .HasForeignKey(d => d.SlsRetailerId);

        }
    }
}
