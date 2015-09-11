using ERPOptima.Model.Accounts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class AnFVoucherDetailMap : EntityTypeConfiguration<AnFVoucherDetail>
    {
        public AnFVoucherDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SubVoucherNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ShortNarration)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("AnFVoucherDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AnFVoucherId).HasColumnName("AnFVoucherId");
            this.Property(t => t.AnFChartOfAccountId).HasColumnName("AnFChartOfAccountId");
            this.Property(t => t.VoucherSerial).HasColumnName("VoucherSerial");
            this.Property(t => t.SubVoucherNumber).HasColumnName("SubVoucherNumber");
            this.Property(t => t.Debit).HasColumnName("Debit");
            this.Property(t => t.Credit).HasColumnName("Credit");
            this.Property(t => t.ShortNarration).HasColumnName("ShortNarration");

            // Relationships
            this.HasRequired(t => t.AnFVoucher)
                .WithMany(t => t.AnFVoucherDetails)
                .HasForeignKey(d => d.AnFVoucherId);

        }
    }
}
