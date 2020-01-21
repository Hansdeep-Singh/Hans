using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HeroBlaze.Models
{
    // You can add profile data for the user by adding more properties to your HeroBlazeUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class HeroBlazeUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<HeroBlazeUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<HeroBlazeUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        //Here you create tables
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserTalent> UserTalents{ get; set; }
        public DbSet<AudioMedia> Audios{ get; set; }
        public DbSet<DocumentMedia> Documents { get; set; }
        public DbSet<ImageMedia> Images { get; set; }
        public DbSet<VideoMedia> Videos { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HeroBlazeUser>().ToTable("HeroBlazeUser").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("HeroBlazeUserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("HeroBlazeClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityRole>().ToTable("UserRoles");
        }
    }
}