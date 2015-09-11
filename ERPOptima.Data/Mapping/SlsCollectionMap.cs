using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsCollectionMap : EntityTypeConfiguration<SlsCollection>
    {
        public SlsCollectionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(32);

                        

            // Table & Column Mappings
            this.ToTable("SlsCollections");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.PaymentMode).HasColumnName("PaymentMode");
            this.Property(t => t.PartyType).HasColumnName("PartyType");
            this.Property(t => t.CollectedFrom).HasColumnName("CollectedFrom");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.TransactionType).HasColumnName("TransactionType");
            this.Property(t => t.TransactionRefNo).HasColumnName("TransactionRefNo");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.CollectionDate).HasColumnName("CollectionDate");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsCollections)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsCollections1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SecCompanies)
                .WithMany(t => t.SlsCollections)
                .HasForeignKey(d => d.SecCompanyId);

        }
    }
}
