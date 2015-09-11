using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsCorporateSalesApplicationMap : EntityTypeConfiguration<SlsCorporateSalesApplication>
    {
        public SlsCorporateSalesApplicationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.SlsCorporateClientId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ExtraExpensePerson)
                .IsRequired()
                .HasMaxLength(70);

            this.Property(t => t.Designation)
                .HasMaxLength(32);

            this.Property(t => t.Phone)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("SlsCorporateSalesApplications");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.SlsCorporateClientId).HasColumnName("SlsCorporateClientId");
            this.Property(t => t.SalesAmount).HasColumnName("SalesAmount");
            this.Property(t => t.PaymentMode).HasColumnName("PaymentMode");
            this.Property(t => t.CreditTerms).HasColumnName("CreditTerms");
            this.Property(t => t.AdvanceAmount).HasColumnName("AdvanceAmount");
            this.Property(t => t.ExtraExpensePerson).HasColumnName("ExtraExpensePerson");
            this.Property(t => t.Designation).HasColumnName("Designation");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Percentage).HasColumnName("Percentage");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ApprovalStatus).HasColumnName("ApprovalStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.HrmEmployee)
                .WithMany(t => t.SlsCorporateSalesApplications)
                .HasForeignKey(d => d.HrmEmployeeId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsCorporateSalesApplications)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsCorporateSalesApplications1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
