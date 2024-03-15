using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class SeriesConfiguration : IEntityTypeConfiguration<Seri>
    {
        public void Configure(EntityTypeBuilder<Seri> builder)
        {
            builder.ToTable(nameof(Seri));
            builder.HasKey(x => x.SeriId);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(1000);
        }
    }
}
