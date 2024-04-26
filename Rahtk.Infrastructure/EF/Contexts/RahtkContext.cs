using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
            //optionsBuilder.AddInterceptors(_publishDomainEventsInterceptors);
            base.OnConfiguring(optionsBuilder);
        }
    }
}

