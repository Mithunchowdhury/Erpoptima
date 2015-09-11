using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsSalesOrderMap : EntityTypeConfiguration<SlsSalesOrder>
    {
        public SlsSalesOrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.TransactionRefNo)
                .IsRequired()
                .HasMaxLength(32);            

            // Table & Column Mappings
            this.ToTable("SlsSalesOrders");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.SlsOfficeId).HasColumnName("SlsOfficeId");
            this.Property(t => t.PartyType).HasColumnName("PartyType");
            this.Property(t => t.Party).HasColumnName("Party");
            this.Property(t => t.PreferredDeliveryDate).HasColumnName("PreferredDeliveryDate");
            this.Property(t => t.SlsCorporateSalesApplicationId).HasColumnName("SlsCorporateSalesApplicationId");
            this.Property(t => t.TransactionType).HasColumnName("TransactionType");
            this.Property(t => t.TransactionRefNo).HasColumnName("TransactionRefNo").IsOptional();
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.Total).HasColumnName("Total");
            this.Property(t => t.Advance).HasColumnName("Advance");
            this.Property(t => t.BankName).HasColumnName("BankName").IsOptional();
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.SecCompnayId).HasColumnName("SecCompnayId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ApprovalStatus).HasColumnName("ApprovalStatus");
            this.Property(t => t.SalesType).HasColumnName("SalesType");
            this.Property(t => t.OptionalPartyName).HasColumnName("OptionalPartyName").IsOptional();
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsSalesOrders)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsSalesOrders1)
                .HasForeignKey(d => d.ModifiedBy);
            this.HasOptional(t => t.SlsCorporateSalesApplication)
                .WithMany(t => t.SlsSalesOrders)
                .HasForeignKey(d => d.SlsCorporateSalesApplicationId);
            this.HasOptional(t => t.SlsOffice)
                .WithMany(t => t.SlsSalesOrders)
                .HasForeignKey(d => d.SlsOfficeId);

        }
    }
}
