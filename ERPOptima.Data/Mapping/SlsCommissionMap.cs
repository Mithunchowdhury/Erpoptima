using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsCommissionMap : EntityTypeConfiguration<SlsCommission>
    {
        public SlsCommissionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChequeNo)
                .HasMaxLength(64);

            this.Property(t => t.Bank)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("SlsCommissions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsDitributorId).HasColumnName("SlsDitributorId");
            this.Property(t => t.SlsDealerId).HasColumnName("SlsDealerId");
            this.Property(t => t.MonthFrom).HasColumnName("MonthFrom");
            this.Property(t => t.MonthTo).HasColumnName("MonthTo");
            this.Property(t => t.YearFrom).HasColumnName("YearFrom");
            this.Property(t => t.YearTo).HasColumnName("YearTo");
            this.Property(t => t.NetSaleAmount).HasColumnName("NetSaleAmount");
            this.Property(t => t.CommissionPercentage).HasColumnName("CommissionPercentage");
            this.Property(t => t.Commission).HasColumnName("Commission");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.ChequeNo).HasColumnName("ChequeNo");
            this.Property(t => t.Bank).HasColumnName("Bank");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsCommissions)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsCommissions1)
                .HasForeignKey(d => d.ModifiedBy);
            //this.HasRequired(t => t.SlsDealer)
            //    .WithMany(t => t.SlsCommissions)
            //    .HasForeignKey(d => d.SlsDealerId);

        }
    }
}
