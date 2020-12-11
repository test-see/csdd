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

        #endregion

        #region data
        //public DbSet<DataIdentityCategory> DataIdentityCategory { get; set; }
        public DbSet<DataProvince> DataProvince { get; set; }
        #endregion

        #region sys
        //public DbSet<DataIdentityCategory> DataIdentityCategory { get; set; }
        public DbSet<SysWhitePhone> SysWhitePhone { get; set; }
        #endregion
        #region hospital
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<HospitalGoods> HospitalGoods { get; set; }
        public DbSet<HospitalDepartment> HospitalDepartment { get; set; }
        public DbSet<HospitalClient> HospitalClient { get; set; }
        #endregion

        //#region tourist
        //public DbSet<Tourist> Tourist { get; set; }
        //public DbSet<TouristClientPreference> TouristClientPreference { get; set; }
        //public DbSet<TouristDepartmentPreference> TouristHospitalPreference { get; set; }
        //public DbSet<TouristSalesPreference> TouristSalesPreference { get; set; }
        //#endregion
    }
}
