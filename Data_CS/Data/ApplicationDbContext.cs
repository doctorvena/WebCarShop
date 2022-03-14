using System;
using System.Collections.Generic;
using System.Text;
using Data_CS.EF_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_CS.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            //  base.OnModelCreating(modelBuilder);

            //  modelBuilder.Entity<User>().HasOne<Administrator>(y => y.Administrator).
            //      WithOne(x => x.User).HasForeignKey<Administrator>(z => z.UserId);

            //  modelBuilder.Entity<User>().HasOne<Client>(y => y.Client).
            //WithOne(x => x.User).HasForeignKey<Client>(z => z.UserId);

            //  modelBuilder.Entity<User>().HasOne<Employee>(y => y.Employee).
            //WithOne(x => x.User).HasForeignKey<Employee>(z => z.UserId);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();

        }
        public DbSet<User> User { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Brand> Brand { get; set; }        
        public DbSet<Car> Car { get; set; }
        public DbSet<CarModel> CarModel { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<DriveType> DriveType { get; set; }
        public DbSet<Fuel> Fuel { get; set; }
        public DbSet<Transmission> Transmission { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<CarImage> CarImage { get; set; }
        public DbSet<ServicedCars> ServicedCars { get; set; }
        public DbSet<UserWishlist> UserWishlist { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
