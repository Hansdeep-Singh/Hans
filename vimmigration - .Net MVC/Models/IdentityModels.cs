using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;


namespace VeraCityImmigration.Models
{
    // You can add profile data for the user by adding more properties to your Client class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Client : IdentityUser
    {
     
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Client> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<Client>
    {
      
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: true)
        {
        }

      public DbSet<ClientDetail> ClientDetails { get; set; }
      public DbSet<Document> Documents { get; set; }
      public DbSet<AdminClientDetail> AdminClientDetails { get; set; }
      public DbSet<AdminClientFee> AdminClientFees { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>().ToTable("Client").Property(p=>p.Id).HasColumnName("ClientId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("VeraCityClientRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("VeraCityClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("VeraCityLogin");
            modelBuilder.Entity<IdentityRole>().ToTable("VeraCityRoles");
        }
    }
}