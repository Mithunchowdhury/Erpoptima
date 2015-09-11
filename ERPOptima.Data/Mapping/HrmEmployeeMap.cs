using ERPOptima.Model.HRM;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class HrmEmployeeMap : EntityTypeConfiguration<HrmEmployee>
    {
        public HrmEmployeeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(128);

            this.Property(t => t.Address)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("HrmEmployees");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.HrmDesignationId).HasColumnName("HrmDesignationId");
            this.Property(t => t.HrmDepartmentId).HasColumnName("HrmDepartmentId");
            this.Property(t => t.SecCompanyId).HasColumnName("SecCompanyId");
            this.Property(t => t.LineManager).HasColumnName("LineManager");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.SlsOfficeId).HasColumnName("SlsOfficeId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.SlsDistributorId).HasColumnName("SlsDistributorId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasOptional(t => t.HrmDepartment)
                .WithMany(t => t.HrmEmployees)
                .HasForeignKey(d => d.HrmDepartmentId);
            this.HasOptional(t => t.HrmDesignation)
                .WithMany(t => t.HrmEmployees)
                .HasForeignKey(d => d.HrmDesignationId);
            this.HasOptional(t => t.HrmEmployee1)
                .WithMany(t => t.HrmEmployees1)
                .HasForeignKey(d => d.LineManager);
            this.HasOptional(t => t.SecCompany)
                .WithMany(t => t.HrmEmployees)
                .HasForeignKey(d => d.SecCompanyId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.HrmEmployees)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.HrmEmployees1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
