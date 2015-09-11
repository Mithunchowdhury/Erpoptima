using ERPOptima.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ERPOptima.Data.Mapping
{
    public class CmnApprovalCommentMap : EntityTypeConfiguration<CmnApprovalComment>
    {
        public CmnApprovalCommentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Comments)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("CmnApprovalComments");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CmnApprovalId).HasColumnName("CmnApprovalId");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.Commentator).HasColumnName("Commentator");
            this.Property(t => t.CommentDate).HasColumnName("CommentDate");
        }
    }
}
