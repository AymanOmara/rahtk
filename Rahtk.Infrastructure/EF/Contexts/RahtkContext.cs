using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rahtk.Domain.Features.Order;
using Rahtk.Domain.Features.Pharmacy;
using Rahtk.Domain.Features.Product;
using Rahtk.Domain.Features.Products;
using Rahtk.Domain.Features.User;

namespace Rahtk.Infrastructure.EF.Contexts
{
    public class RahtkContext : IdentityDbContext<RahtkUser>
    {
        public RahtkContext(DbContextOptions<RahtkContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
               .ApplyConfigurationsFromAssembly(typeof(RahtkContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<CategoryEntity> Categories { set; get; }

        public DbSet<ProductEntity> Products { set; get; }

        public DbSet<FavoriteProductUser> FavoriteProductUser { get; set; }

        public DbSet<AddressEntity> Addresses { get; set; }

        public DbSet<PaymentOptionEntity> PaymentOptions { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<DrugEntity> Drugs { get; set; }

        public DbSet<ReminderEntity> Reminders { get; set; }

        public DbSet<NotificationEntity> Notifications { get; set; }
    }
}