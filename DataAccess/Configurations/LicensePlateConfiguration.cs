using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class LicensePlateConfiguration : IEntityTypeConfiguration<LicensePlate>
    {
        public void Configure(EntityTypeBuilder<LicensePlate> builder)
        {
            builder.ToTable(nameof(LicensePlate));
            builder.HasKey(x => x.LicensePlateId);
            builder.Property(x => x.LicensePlateNumber).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.CreatedDate).IsRequired();

            #region Relationship
            builder.HasOne(x => x.District).WithMany(x => x.LicensePlates).HasForeignKey(x => x.DistrictId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Account).WithMany(x => x.LicensePlates).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Series).WithMany(x => x.LicensePlates).HasForeignKey(x => x.SeriesId).OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
