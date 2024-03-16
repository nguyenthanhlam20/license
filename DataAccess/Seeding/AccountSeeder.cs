using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Seeding
{
    public class AccountSeeder
    {
        private readonly ModelBuilder modelBuilder;

        public AccountSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            // Seed role data
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>() { Id = Guid.Parse("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA"), Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<Guid>() { Id = Guid.Parse("D2D63C5B-D09B-4828-8322-F18BA103FE86"), Name = "User", NormalizedName = "USER" }
            );

            //Seed Account
            modelBuilder.Entity<Account>().HasData(
                new Account()
                {
                    Id = Guid.Parse("B8C777A9-55B9-4B3D-860A-D7B56E4C24B7"),
                    UserName = "admin",
                    NormalizedUserName = "ICPDPHN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    Password = "admin",
                    PasswordHash = new PasswordHasher<Account>().HashPassword(null, "admin"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Fullname = "Admin",
                    IsAccountActive = true,
                    JoinedDate = DateTime.Now
                },
                 new Account()
                 {
                     Id = Guid.Parse("34DD158A-6B96-4149-A3B4-5D1B5CC374A3"),
                     UserName = "thanhdao",
                     NormalizedUserName = "THANHDAO",
                     Email = "thanhdao@gmail.com",
                     NormalizedEmail = "THANHDAO@GMAIL.COM",
                     EmailConfirmed = true,
                     Password = "thanhdao",
                     PasswordHash = new PasswordHasher<Account>().HashPassword(null, "thanhdao"),
                     SecurityStamp = Guid.NewGuid().ToString(),
                     Fullname = "Thành Đào",
                     IsAccountActive = true,
                     JoinedDate = DateTime.Now
                 },
                   new Account()
                   {
                       Id = Guid.Parse("48DA158A-6B96-4149-A3B4-5D1B5CC374A3"),
                       UserName = "thuylinh",
                       NormalizedUserName = "THUYLINH",
                       Email = "thuylinh@gmail.com",
                       NormalizedEmail = "THUYLINH@GMAIL.COM",
                       EmailConfirmed = true,
                       Password = "thuylinh",
                       PasswordHash = new PasswordHasher<Account>().HashPassword(null, "thuylinh"),
                       SecurityStamp = Guid.NewGuid().ToString(),
                       Fullname = "Thùy Linh",
                       IsAccountActive = true,
                       JoinedDate = DateTime.Now
                   }

                );

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid> { UserId = Guid.Parse("B8C777A9-55B9-4B3D-860A-D7B56E4C24B7"), RoleId = Guid.Parse("B8FD818F-63F1-49EE-BEC5-F7B66CAFBFCA") },
                new IdentityUserRole<Guid> { UserId = Guid.Parse("34DD158A-6B96-4149-A3B4-5D1B5CC374A3"), RoleId = Guid.Parse("D2D63C5B-D09B-4828-8322-F18BA103FE86") },
                new IdentityUserRole<Guid> { UserId = Guid.Parse("48DA158A-6B96-4149-A3B4-5D1B5CC374A3"), RoleId = Guid.Parse("D2D63C5B-D09B-4828-8322-F18BA103FE86") }
                );
        }
    }
}