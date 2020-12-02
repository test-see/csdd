using foundation.ef5.poco;
using Microsoft.EntityFrameworkCore;

namespace foundation.ef5
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext() { }
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options) { }

        public DbSet<Client> Client { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserVerificationCode> UserVerificationCode { get; set; }
        public DbSet<DataIdentityCategory> DataIdentityCategory { get; set; }
        public DbSet<DataWhitePhone> DataWhitePhone { get; set; }
    }
}
