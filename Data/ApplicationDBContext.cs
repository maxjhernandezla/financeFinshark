using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

            builder
                .Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);

            builder
                .Entity<Portfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" },
            };
            List<Stock> stocks = new List<Stock>
            {
                new Stock
                {
                    Id = 1,
                    Symbol = "MSFT",
                    CompanyName = "Microsoft",
                    Industry = "Technology",
                    LastDiv = 1,
                    MarketCap = 2121231545,
                    Purchase = 3
                },
                new Stock
                {
                    Id = 2,
                    Symbol = "AMZN",
                    CompanyName = "Amazon",
                    Industry = "Technology",
                    LastDiv = 1.2m,
                    MarketCap = 95421575932,
                    Purchase = 21
                },
                new Stock
                {
                    Id = 3,
                    Symbol = "SHL",
                    CompanyName = "Shell",
                    Industry = "Petrol",
                    LastDiv = 1.5m,
                    MarketCap = 7584265157,
                    Purchase = 6
                },
                new Stock
                {
                    Id = 4,
                    Symbol = "APL",
                    CompanyName = "Apple",
                    Industry = "Technology",
                    LastDiv = 0.9m,
                    MarketCap = 45751547515,
                    Purchase = 25
                },
                new Stock
                {
                    Id = 5,
                    Symbol = "AA2000",
                    CompanyName = "Aerolineas Argentinas",
                    Industry = "Transport",
                    LastDiv = 1.4m,
                    MarketCap = 75455155545,
                    Purchase = 5
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
            builder.Entity<Stock>().HasData(stocks);
        }
    }
}
