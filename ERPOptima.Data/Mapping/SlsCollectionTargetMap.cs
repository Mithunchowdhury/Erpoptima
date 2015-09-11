using ERPOptima.Model.Sales;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class SlsCollectionTargetMap : EntityTypeConfiguration<SlsCollectionTarget>
    {
        public SlsCollectionTargetMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SlsCollectionTargets");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.HrmEmployeeId).HasColumnName("HrmEmployeeId");
            this.Property(t => t.TargetCollectionAmount).HasColumnName("TargetCollectionAmount");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.HrmEmployee)
                .WithMany(t => t.SlsCollectionTargets)
                .HasForeignKey(d => d.HrmEmployeeId);
            this.HasRequired(t => t.SecUser)
                .WithMany(t => t.SlsCollectionTargets)
                .HasForeignKey(d => d.CreatedBy).WillCascadeOnDelete(false);
            this.HasOptional(t => t.SecUser1)
                .WithMany(t => t.SlsCollectionTargets1)
                .HasForeignKey(d => d.ModifiedBy);

        }
    }
}
