using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsAreaConfigurationDetailMap : EntityTypeConfiguration<SlsAreaConfigurationDetail>
    {
        public SlsAreaConfigurationDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BasedOn)
                .IsRequired()
                .HasMaxLength(12);

            // Table & Column Mappings
            this.ToTable("SlsAreaConfigurationDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BasedOn).HasColumnName("BasedOn");
            this.Property(t => t.RefId).HasColumnName("RefId");
            this.Property(t => t.SlsAreaConfigurationId).HasColumnName("SlsAreaConfigurationId");
        }
    }
}
