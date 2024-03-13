using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(Account));
            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(100);
            builder.Property(x => x.IsAccountActive).IsRequired(true);
            builder.Property(x => x.JoinedDate).IsRequired(true);
        }
    }
}
