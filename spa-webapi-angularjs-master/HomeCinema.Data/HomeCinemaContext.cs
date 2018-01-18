using HomeCinema.Data.Configurations;
using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class HomeCinemaContext : DbContext
    {
        public HomeCinemaContext()
            : base("HomeCinema")
        {
            Database.SetInitializer<HomeCinemaContext>(null);
        }

        #region Entity Sets
        public IDbSet<User> UserSet { get; set; }
        public IDbSet<Role> RoleSet { get; set; }
        public IDbSet<UserRole> UserRoleSet { get; set; }
        public IDbSet<Profiler> ProfileSet { get; set; }
        public IDbSet<Movie> MovieSet { get; set; }
        public IDbSet<Genre> GenreSet { get; set; }
        public IDbSet<Stock> StockSet { get; set; }
        public IDbSet<Rental> RentalSet { get; set; }
        public IDbSet<Error> ErrorSet { get; set; }
        public IDbSet<Calendar> CalendarSet { get; set; }
        public IDbSet<Image> ImageSet { get; set; }
        public IDbSet<Sys_Unit> Sys_UnitSet { get; set; }
        public IDbSet<Key_Sys_Unit> Key_Sys_UnitSet { get; set; }
        public IDbSet<Sys_User> Sys_UserSet { get; set; }
        public IDbSet<Lession> LessionSet { get; set; }
        public IDbSet<CalenderLession> CalenderLessionSet { get; set; }
        public IDbSet<Classroom> ClassroomSet { get; set; }
        public IDbSet<School> SchoolSet { get; set; }
        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            //modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new MovieConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new StockConfiguration());
            modelBuilder.Configurations.Add(new RentalConfiguration());
            modelBuilder.Configurations.Add(new ImageConfiguration());
            modelBuilder.Configurations.Add(new Sys_UnitConfiguration());
            modelBuilder.Configurations.Add(new Sys_UserConfiguration());
            //modelBuilder.Configurations.Add(new CalenderLessionConfiguration());

        }
    }
}
