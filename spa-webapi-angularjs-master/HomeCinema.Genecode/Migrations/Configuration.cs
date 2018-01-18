namespace HomeCinema.Genecode.Migrations
{
    using FizzWare.NBuilder;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HomeCinema.Genecode.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HomeCinema.Genecode.Models.ApplicationDbContext context)
        {
            var key_sys_units = Builder<Key_Sys_Unit>.CreateListOfSize(5)
        .Build();
            
            context.Key_Sys_Unit.AddOrUpdate(c => c.ID, key_sys_units.ToArray());
        }
    }
}
