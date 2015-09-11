using ERPOptima.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class CmnProjectMap : EntityTypeConfiguration<CmnProject>
    {
        public CmnProjectMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Location)
                .HasMaxLength(256);

            this.Property(t => t.Prefix)
                .HasMaxLength(8);

            this.Property(t => t.Description)
                .HasMaxLength(256);

            this.Property(t => t.ClosingNote)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("CmnProjects");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CmnBusinessesId).HasColumnName("CmnBusinessesId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Prefix).HasColumnName("Prefix");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ClosingStatus).HasColumnName("ClosingStatus");
            this.Property(t => t.ClosedBy).HasColumnName("ClosedBy");
            this.Property(t => t.ClosingNote).HasColumnName("ClosingNote");
            this.Property(t => t.ClosingDate).HasColumnName("ClosingDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.CmnBusiness)
                .WithMany(t => t.CmnProjects)
                .HasForeignKey(d => d.CmnBusinessesId);

        }
    }
}
