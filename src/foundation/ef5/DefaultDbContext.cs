using foundation.ef5.poco;
using Microsoft.EntityFrameworkCore;

namespace foundation.ef5
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext() { }
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options) { }

        #region client
        public DbSet<Client> Client { get; set; }

        #endregion

        #region user
        public DbSet<User> User { get; set; }
        public DbSet<UserVerificationCode> UserVerificationCode { get; set; }

        #endregion

        #region data
        public DbSet<DataIdentityCategory> DataIdentityCategory { get; set; }
        public DbSet<DataWhitePhone> DataWhitePhone { get; set; }

        #endregion

        #region tourist
        public DbSet<Tourist> Tourist { get; set; }
        public DbSet<TouristClientPreference> TouristClientPreference { get; set; }
        public DbSet<TouristHospitalPreference> TouristHospitalPreference { get; set; }
        public DbSet<TouristSalesPreference> TouristSalesPreference { get; set; }
        #endregion
    }
}
