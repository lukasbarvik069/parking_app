using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using parking_app.Models;

namespace parking_app.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
        public DbSet<ParkingPlace> ParkingPlaces { get; set; }
        public DbSet<Spz> Spz { get; set; }
        public DbSet<SpzParkingPlace> SpzParkingPlaces { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParkingPlace>(pp =>
            {
                pp.OwnsOne(i => i.Address, a =>
                {
                    a.Property(ad => ad.City).HasColumnName("city");
                    a.Property(ad => ad.Street).HasColumnName("street");
                    a.Property(ad => ad.PostalCode).HasColumnName("postalCode");
                    a.Property(ad => ad.HouseNumber).HasColumnName("houseNumber");
                });
            });


            modelBuilder.Entity<User>()
                .HasMany(u => u.Spzs)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Spz>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Spz>()
                .HasMany(s => s.SpzParkingPlaces)
                .WithOne(spp => spp.Spz)
                .HasForeignKey(spp => spp.SpzId);

            modelBuilder.Entity<ParkingPlace>()
                .HasMany(pp => pp.SpzParkingPlaces)
                .WithOne(spp => spp.ParkingPlace)
                .HasForeignKey(spp => spp.ParkingPlaceId);

            modelBuilder.Entity<SpzParkingPlace>(e =>
            {
                e.OwnsOne(date => date.Date , d =>
                {
                    d.Property(i => i.Start).HasColumnName("start");
                    d.Property(i => i.End).HasColumnName("end");
                });
            });

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "User", NormalizedName = "USER" }
            );

            var hasher = new PasswordHasher<User>();
            var admin = new User
            {
                Id = 1,
                UserName = "admin@parking.cz",
                NormalizedUserName = "ADMIN@PARKING.CZ",
                Email = "admin@parking.cz",
                NormalizedEmail = "ADMIN@PARKING.CZ",
                EmailConfirmed = true,
                FName = "Hlavní",
                LName = "Admin",
                SecurityStamp = "admin_guid"
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Heslo123!");

            var user = new User
            {
                Id = 2,
                UserName = "pepa@seznam.cz",
                NormalizedUserName = "PEPA@SEZNAM.CZ",
                Email = "pepa@seznam.cz",
                NormalizedEmail = "PEPA@SEZNAM.CZ",
                EmailConfirmed = true,
                FName = "Pepa",
                LName = "Novák",
                SecurityStamp = "user_guid"
            };
            user.PasswordHash = hasher.HashPassword(user, "Heslo123!");

            modelBuilder.Entity<User>()
                .HasData(admin, user);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
                new IdentityUserRole<int> { UserId = 2, RoleId = 2 }
);



            modelBuilder.Entity<ParkingPlace>().HasData(
                new { Id = 1, Name = "Centrum - Hlavní", Capacity = 50, PricePerHour = 40 },
                new { Id = 2, Name = "OC Nová Karolina", Capacity = 200, PricePerHour = 0 },
                new { Id = 3, Name = "Letiště Václava Havla", Capacity = 500, PricePerHour = 80 },
                new { Id = 4, Name = "Nádraží Svinov", Capacity = 30, PricePerHour = 20 },
                new { Id = 5, Name = "Podzemní garáže Divadlo", Capacity = 45, PricePerHour = 50 }
            );

            modelBuilder.Entity<ParkingPlace>().OwnsOne(p => p.Address).HasData(
                new { ParkingPlaceId = 1, City = "Praha", Street = "Václavské náměstí", PostalCode = "110 00", HouseNumber = "1" },
                new { ParkingPlaceId = 2, City = "Ostrava", Street = "Jantarová", PostalCode = "702 00", HouseNumber = "4" },
                new { ParkingPlaceId = 3, City = "Praha", Street = "Aviatická", PostalCode = "161 00", HouseNumber = "6" },
                new { ParkingPlaceId = 4, City = "Ostrava", Street = "Peterkova", PostalCode = "721 00", HouseNumber = "79" },
                new { ParkingPlaceId = 5, City = "Brno", Street = "Rooseveltova", PostalCode = "602 00", HouseNumber = "17" }
            );



        }
    }
}
