﻿using foundation.ef5.poco;
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
        public DbSet<UserRole> UserRole { get; set; }
        #endregion

        #region data
        public DbSet<DataMenu> DataMenu { get; set; }
        public DbSet<DataAuthorizeRole> DataAuthorizeRole { get; set; }
        #endregion

        #region sys
        public DbSet<SysWhitePhone> SysWhitePhone { get; set; }
        public DbSet<SysRole> SysRole { get; set; }
        public DbSet<SysPrivilege> SysPrivilege { get; set; }
        public DbSet<SysConfig> SysConfig { get; set; }
        #endregion

        #region hospital
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<HospitalGoods> HospitalGoods { get; set; }
        public DbSet<HospitalDepartment> HospitalDepartment { get; set; }
        public DbSet<HospitalClient> HospitalClient { get; set; }
        #endregion

        #region eventlog
        public DbSet<Eventlog> Eventlog { get; set; }
        #endregion
    }
}
