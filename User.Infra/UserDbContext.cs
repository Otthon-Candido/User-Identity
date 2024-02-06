using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Infra.Entities;

namespace User.Infra.Models
{

    public class UserDbContext : IdentityDbContext<UserEntity>
    {

        public UserDbContext(DbContextOptions<UserDbContext> opts) : base(opts)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            string adminRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(
                        new IdentityRole { Id = adminRoleId, Name = "admin", NormalizedName = "ADMIN" },
                        new IdentityRole { Id = userRoleId, Name = "user", NormalizedName = "USER" }
                    // Add more roles as needed
                    );

            var hasher = new PasswordHasher<UserEntity>();
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = userId,
                    UserName = "teste@gmail.com",
                    NormalizedUserName = "TESTE@GMAIL.COM",
                    Email = "teste@gmail.com",
                    NormalizedEmail = "TESTE@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Teste123*"),
                    NickName = "Teste",
                    SecurityStamp = string.Empty
                }
            );

            // Assign admin user to Admin role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId, // Admin role ID
                    UserId = userId
                }
            );
        }

    }
}
