using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsNotificationMap : EntityTypeConfiguration<SlsNotification>
    {
        public SlsNotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Message)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.URL)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SlsNotifications");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.URL).HasColumnName("URL");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Date).HasColumnName("Date");
        }
    }
}
