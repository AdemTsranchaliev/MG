using ArticalesApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArticalesApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Аrticle> Аrticle { get; set; }
        public DbSet<User> User { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=ArticalesDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            string ADMIN_ID = "02174cf0–9412–4cfe - afbf - 59f706d72cf6";
            string ROLE_ID = "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6";

            //seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            },
            new IdentityRole
            {
                Name = "Reviewer"
            },
            new IdentityRole
            {
                Name = "Author"
            });

            //create user
            var appUser = new User
            {
                Id = ADMIN_ID,
                Email = "teacher@article.com",
                EmailConfirmed = true,
                FirstName = "Teacher",
                LastName = "Teacher",
                UserName = "teacher@article.com",
                NormalizedEmail = "TEACHER@ARTICLE.COM",
                NormalizedUserName = "TEACHER@ARTICLE.COM"
            };

            //set user password
            PasswordHasher<User> ph = new PasswordHasher<User>();
            appUser.PasswordHash = ph.HashPassword(appUser, "Teacher!2");

            //seed user
            builder.Entity<User>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }

        public DbSet<ArticalesApp.Models.CreateRoleViewModel> CreateRoleViewModel { get; set; }
    }
}
