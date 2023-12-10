using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Users.Domain
{
    /// <inheritdoc />
    public class AuthDbContext: DbContext
    {
        /// <inheritdoc />
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// User table
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}