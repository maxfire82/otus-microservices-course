using Microsoft.EntityFrameworkCore;
using Users.Domain.Models;

namespace Users.Domain
{
    /// <inheritdoc />
    public class UserDbContext: DbContext
    {
        /// <inheritdoc />
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// User table
        /// </summary>
        public DbSet<Models.User> Users { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}