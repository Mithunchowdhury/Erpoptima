using ERPOptima.Model.Accounts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    //public class AnFBankReconciliationItemMap : EntityTypeConfiguration<AnFBankReconciliationItem>
    //{
    //    public AnFBankReconciliationItemMap()
    //    {
    //        // Primary Key
    //        this.HasKey(t => t.Id);

    //        // Properties
    //        this.Property(t => t.Id)
    //            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

    //        this.Property(t => t.Name)
    //            .HasMaxLength(128);

    //        // Table & Column Mappings
    //        this.ToTable("AnFBankReconciliationItems");
    //        this.Property(t => t.Id).HasColumnName("Id");
    //        this.Property(t => t.AnFChartOfAccountId).HasColumnName("AnFChartOfAccountId");
    //        this.Property(t => t.Name).HasColumnName("Name");
    //        this.Property(t => t.AddOrLess).HasColumnName("AddOrLess");
    //        this.Property(t => t.Status).HasColumnName("Status");
    //        this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
    //        this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
    //        this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
    //        this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

    //        // Relationships
    //        this.HasOptional(t => t.AnFChartOfAccount)
    //            .WithMany(t => t.AnFBankReconciliationItems)
    //            .HasForeignKey(d => d.AnFChartOfAccountId);

    //    }
    //}
}
