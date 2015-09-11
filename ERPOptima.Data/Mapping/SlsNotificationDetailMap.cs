using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsNotificationDetailMap : EntityTypeConfiguration<SlsNotificationDetail>
    {
        public SlsNotificationDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsNotificationDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SlsNotificationId).HasColumnName("SlsNotificationId");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.IsRead).HasColumnName("IsRead");

            // Relationships
            this.HasRequired(t => t.HrmEmployee)
                .WithMany(t => t.SlsNotificationDetails)
                .HasForeignKey(d => d.HrmEmployeeId);
            this.HasRequired(t => t.SlsNotification)
                .WithMany(t => t.SlsNotificationDetails)
                .HasForeignKey(d => d.SlsNotificationId);

        }
    }
}
