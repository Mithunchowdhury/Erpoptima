using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsCorporateClientMap : EntityTypeConfiguration<SlsCorporateClient>
    {
        public SlsCorporateClientMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.BusinessType)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Email)
                .HasMaxLength(32);

            this.Property(t => t.Phone)
                .HasMaxLength(20);

            this.Property(t => t.ContactPerson)
                .HasMaxLength(64);

            this.Property(t => t.ContactPersonEmail)
                .HasMaxLength(32);

            this.Property(t => t.ContactPersonPhone)
                .HasMaxLength(20);

            this.Property(t => t.BankName)
                .HasMaxLength(70);

            this.Property(t => t.BankAccount)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("SlsCorporateClients");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SlsDistrictId).HasColumnName("SlsDistrictId");
            this.Property(t => t.SlsCorporateTypeId).HasColumnName("SlsCorporateTypeId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.BusinessType).HasColumnName("BusinessType");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.ContactPerson).HasColumnName("ContactPerson");
            this.Property(t => t.ContactPersonEmail).HasColumnName("ContactPersonEmail");
            this.Property(t => t.ContactPersonPhone).HasColumnName("ContactPersonPhone");
            this.Property(t => t.ExecutiveName).HasColumnName("ExecutiveName");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankAccount).HasColumnName("BankAccount");
            this.Property(t => t.DefaultCreditLimit).HasColumnName("DefaultCreditLimit");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsCorporateClients)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsCorporateClients1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasRequired(t => t.SlsCorporateType)
                .WithMany(t => t.SlsCorporateClients)
                .HasForeignKey(d => d.SlsCorporateTypeId);
            this.HasRequired(t => t.SlsDistrict)
                .WithMany(t => t.SlsCorporateClients)
                .HasForeignKey(d => d.SlsDistrictId);

        }
    }
}
