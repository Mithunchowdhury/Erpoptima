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
    public class CmnAccountHeadMapperMap : EntityTypeConfiguration<CmnAccountHeadMapper>
    {
        public CmnAccountHeadMapperMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Remarks)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("CmnAccountHeadMappers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.AnFChartOfAccountId).HasColumnName("AnFChartOfAccountId");
            this.Property(t => t.CnmMappingTypeId).HasColumnName("CnmMappingTypeId");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // Relationships
            this.HasOptional(t => t.AnFChartOfAccount)
                .WithMany(t => t.CmnAccountHeadMappers)
                .HasForeignKey(d => d.AnFChartOfAccountId);
            this.HasOptional(t => t.CmnMappingType)
                .WithMany(t => t.CmnAccountHeadMappers)
                .HasForeignKey(d => d.CnmMappingTypeId);
            this.HasOptional(t => t.SecCompany)
                .WithMany(t => t.CmnAccountHeadMappers)
                .HasForeignKey(d => d.SecCompanyId);

        }
    }
}
