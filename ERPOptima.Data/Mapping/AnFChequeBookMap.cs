using ERPOptima.Model.Accounts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    //public class AnFChequeBookMap : EntityTypeConfiguration<AnFChequeBook>
    //{
    //    public AnFChequeBookMap()
    //    {
    //        // Primary Key
    //        this.HasKey(t => t.Id);

    //        // Properties
    //        this.Property(t => t.Id)
    //            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

    //        this.Property(t => t.ChequeBookNo)
    //            .HasMaxLength(64);

    //        this.Property(t => t.StartingPageNo)
    //            .HasMaxLength(32);

    //        this.Property(t => t.EndingPageNo)
    //            .HasMaxLength(32);

    //        // Table & Column Mappings
    //        this.ToTable("AnFChequeBooks");
    //        this.Property(t => t.Id).HasColumnName("Id");
    //        this.Property(t => t.ChequeBookNo).HasColumnName("ChequeBookNo");
    //        this.Property(t => t.AnFChartOfAccountId).HasColumnName("AnFChartOfAccountId");
    //        this.Property(t => t.IssueDate).HasColumnName("IssueDate");
    //        this.Property(t => t.NoofPage).HasColumnName("NoofPage");
    //        this.Property(t => t.StartingPageNo).HasColumnName("StartingPageNo");
    //        this.Property(t => t.EndingPageNo).HasColumnName("EndingPageNo");
    //        this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
    //        this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
    //        this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
    //        this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

    //        // Relationships
    //        this.HasOptional(t => t.AnFChartOfAccount)
    //            .WithMany(t => t.AnFChequeBooks)
    //            .HasForeignKey(d => d.AnFChartOfAccountId);

    //    }
    //}
}
