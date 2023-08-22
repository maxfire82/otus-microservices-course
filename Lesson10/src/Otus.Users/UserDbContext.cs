using Microsoft.EntityFrameworkCore;
using Otus.Users.Models;

namespace Otus.Users
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
        public DbSet<User> Users { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}