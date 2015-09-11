using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsDealerMap : EntityTypeConfiguration<SlsDealer>
    {
        public SlsDealerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Phone)
                .HasMaxLength(20);

            this.Property(t => t.ResponsiblePerson)
                .HasMaxLength(70);

            this.Property(t => t.BankName)
                .HasMaxLength(70);

            this.Property(t => t.BankAccount)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("SlsDealers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsThanaId).HasColumnName("SlsThanaId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.HeadOfficeId).HasColumnName("HeadOfficeId");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.ResponsiblePerson).HasColumnName("ResponsiblePerson");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankAccount).HasColumnName("BankAccount");
            this.Property(t => t.CreditLimit).HasColumnName("CreditLimit");
            this.Property(t => t.RateOfCommission).HasColumnName("RateOfCommission");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.IsAllCompany).HasColumnName("IsAllCompany");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.SecurityDeposit).HasColumnName("SecurityDeposit");

            // Relationships
            this.HasRequired(t => t.SecCompany)
                .WithMany(t => t.SlsDealers)
                .HasForeignKey(d => d.SecCompanyId);
            this.HasRequired(t => t.SecCompany1)
                .WithMany(t => t.SlsDealers1)
                .HasForeignKey(d => d.SecCompanyId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsDealers)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsDealers1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SlsOffice)
                .WithMany(t => t.SlsDealers)
                .HasForeignKey(d => d.HeadOfficeId);
            this.HasOptional(t => t.SlsOffice1)
                .WithMany(t => t.SlsDealers1)
                .HasForeignKey(d => d.HeadOfficeId);
            this.HasRequired(t => t.SlsThana)
                .WithMany(t => t.SlsDealers)
                .HasForeignKey(d => d.SlsThanaId);
            this.HasRequired(t => t.SlsThana1)
                .WithMany(t => t.SlsDealers1)
                .HasForeignKey(d => d.SlsThanaId);

        }
    }
}
