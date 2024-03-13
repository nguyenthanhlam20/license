using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class LicenseConfiguration : IEntityTypeConfiguration<License>
    {
        public void Configure(EntityTypeBuilder<License> builder)
        {
            builder.ToTable(nameof(License));
            builder.HasKey(x => x.LicenseId);
            builder.Property(x => x.LicenseNumber).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.CreatedDate).IsRequired();

            #region Relationship
            builder.HasOne(x => x.District).WithMany(x => x.Licenses).HasForeignKey(x => x.DistrictId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Account).WithMany(x => x.Licenses).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
