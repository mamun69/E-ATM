using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVC_EATM.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            
        }
    }
}