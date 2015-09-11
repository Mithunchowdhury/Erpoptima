using ERPOptima.Model.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPOptima.Data.Mapping
{
    public class CmnMappingTypeMap : EntityTypeConfiguration<CmnMappingType>
    {
        public CmnMappingTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(128);

            this.Property(t => t.Remarks)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("CmnMappingTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
        }
    }
}
