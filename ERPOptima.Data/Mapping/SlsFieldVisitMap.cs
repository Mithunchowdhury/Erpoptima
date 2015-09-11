using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsFieldVisitMap : EntityTypeConfiguration<SlsFieldVisit>
    {
        public SlsFieldVisitMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefNo)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.CustomerName)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.CustomerMobileNo)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("SlsFieldVisits");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefNo).HasColumnName("RefNo");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.VisitDate).HasColumnName("VisitDate");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.CustomerDetails).HasColumnName("CustomerDetails");
            this.Property(t => t.CustomerMobileNo).HasColumnName("CustomerMobileNo");
            this.Property(t => t.VisitDetails).HasColumnName("VisitDetails");
            this.Property(t => t.FollowupDate).HasColumnName("FollowupDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.HrmEmployee)
                .WithMany(t => t.SlsFieldVisits)
                .HasForeignKey(d => d.HrmEmployeeId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsFieldVisits)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsFieldVisits1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
