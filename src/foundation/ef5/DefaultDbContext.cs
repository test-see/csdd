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
        public DbSet<ClientGoods> ClientGoods { get; set; }
        public DbSet<ClientMapping> ClientMapping { get; set; }
        public DbSet<ClientMappingGoods> ClientMappingGoods { get; set; }
        #endregion

        #region user
        public DbSet<User> User { get; set; }
        public DbSet<UserVerificationCode> UserVerificationCode { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserHospital> UserHospital { get; set; }
        public DbSet<UserClient> UserClient { get; set; }
        #endregion

        #region data
        public DbSet<DataMenu> DataMenu { get; set; }
        public DbSet<DataPortal> DataPortal { get; set; }
        public DbSet<DataDepartmentType> DataDepartmentType { get; set; }
        public DbSet<DataPurchaseThresholdType> DataPurchaseThresholdType { get; set; }
        public DbSet<DataStoreChangeType> DataStoreChangeType { get; set; }
        public DbSet<DataInvoiceType> DataInvoiceType { get; set; }
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
        public DbSet<HospitalGoodsClient> HospitalGoodsClient { get; set; }
        public DbSet<HospitalDepartment> HospitalDepartment { get; set; }
        public DbSet<HospitalClient> HospitalClient { get; set; }
        #endregion

        #region eventlog
        public DbSet<Eventlog> Eventlog { get; set; }
        public DbSet<EventlogHospitalGoods> EventlogHospitalGoods { get; set; }
        #endregion

        #region purchase
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PurchaseGoods> PurchaseGoods { get; set; }
        public DbSet<PurchaseGoodsBillno> PurchaseGoodsBillno { get; set; }
        public DbSet<PurchaseSetting> PurchaseSetting { get; set; }
        public DbSet<PurchaseSettingThreshold> PurchaseSettingThreshold { get; set; }
        #endregion

        #region store
        public DbSet<Store> Store { get; set; }
        public DbSet<StoreRecord> StoreRecord { get; set; }
        public DbSet<StoreInout> StoreInout { get; set; }
        public DbSet<StoreInoutGoods> StoreInoutGoods { get; set; }
        public DbSet<StoreRecordBillno> StoreRecordBillno { get; set; }
        #endregion

        #region prescription
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<PrescriptionGoods> PrescriptionGoods { get; set; }
        #endregion

        #region checklist
        public DbSet<CheckList> CheckList { get; set; }
        public DbSet<CheckListGoods> CheckListGoods { get; set; }
        #endregion

        #region invoice
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceReport> InvoiceReport { get; set; }
        public DbSet<InvoiceReportRecord> InvoiceReportRecord { get; set; }
        #endregion
    }
}
