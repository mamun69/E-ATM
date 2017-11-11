using MVC_EATM.Models;

namespace MVC_EATM.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_EATM.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVC_EATM.Models.ApplicationDbContext context)
        {
            context.Accounts.AddOrUpdate(a=>a.Id,
                new Account() { Id = 1, accountNo = 100, pin = 123, balance = 10000},
                new Account() { Id = 2, accountNo = 101, pin = 234, balance = 5000},
                new Account() { Id = 3, accountNo = 102, pin = 345, balance = 8000},
                new Account() { Id = 4, accountNo = 103, pin = 456, balance = 8000 }
                );
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
