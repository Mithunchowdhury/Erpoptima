using ERPOptima.Model.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class InvDamageDetailMap : EntityTypeConfiguration<InvDamageDetail>
    {
        public InvDamageDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("InvDamageDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InvDamageId).HasColumnName("InvDamageId");
            this.Property(t => t.SlsProductId).HasColumnName("SlsProductId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.SlsUnitsId).HasColumnName("SlsUnitsId");
            this.Property(t => t.Reason).HasColumnName("Reason");
           

            // Relationships
            this.HasRequired(t => t.InvDamage)
                .WithMany(t => t.InvDamageDetails)
                .HasForeignKey(d => d.InvDamageId);


           

        }
    }
}
