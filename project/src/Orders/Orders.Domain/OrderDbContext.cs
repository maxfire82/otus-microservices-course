using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entity;

namespace Orders.Domain
{
    /// <inheritdoc />
    public class OrderDbContext: DbContext
    {
        /// <inheritdoc />
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Orders table
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
            base.OnModelCreating(builder);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }
    }
}