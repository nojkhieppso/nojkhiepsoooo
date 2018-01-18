
    using HomeCinema.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
namespace HomeCinema.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<HomeCinemaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HomeCinemaContext context)
        {
            context.UserSet.AddOrUpdate(u => u.Email, new User[]{
                new User()
                {
                    Id=Guid.NewGuid(),
                    Email="thanhhuyen9197@gmail.com",
                    Username="nojkhiepso",
                    HashedPassword ="pyQg82zsjZKdsogFff88PfP1L6zHHhyEphxi6RIKlU0=",
                    Salt = "C/dFisU88iM2CkOFwUZsfQ==",
                    IsLocked = false,
                    DateCreated = DateTime.Now
                }
            });
        }
       
        
    }
}
