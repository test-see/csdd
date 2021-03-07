using domain.store;
using domain.store.enums;
using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.prescription;
using irespository.prescription.model;
using irespository.prescription.profile.enums;
using irespository.store.model;
using irespository.store.profile.model;
using System.Linq;

namespace domain.prescription
{
    public class PrescriptionContext
    {
        private readonly IPrescriptionRespository _prescriptionRespository;
        private readonly StoreContext _storeContext;
        private readonly DefaultDbTransaction _defaultDbTransaction;
        private readonly IPrescriptionGoodsRespository _prescriptionGoodsRespository;
        public PrescriptionContext(IPrescriptionRespository prescriptionRespository,
            StoreContext storeContext,
            DefaultDbTransaction defaultDbTransaction,
            IPrescriptionGoodsRespository prescriptionGoodsRespository)
        {
            _prescriptionRespository = prescriptionRespository;
            _storeContext = storeContext;
            _defaultDbTransaction = defaultDbTransaction;
            _prescriptionGoodsRespository = prescriptionGoodsRespository;
        }

        public PagerResult<PrescriptionListApiModel> GetPagerList(PagerQuery<PrescriptionListQueryModel> query, int hospitalId)
        {
            return _prescriptionRespository.GetPagerList(query, hospitalId);
        }

        public Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId)
        {
            return _prescriptionRespository.Create(created, departmentId, userId);
        }

        public int Submit(int id, int userId)
        {
            var model = _prescriptionRespository.Get(id);
            var list = _prescriptionGoodsRespository.GetList(id);
            var goods = list.Select(x => new StoreChangeGoodsValueModel
            {
                HospitalGoodId = x.HospitalGoodsId,
                Qty = x.Qty,
            });
            using (var trans = _defaultDbTransaction.Begin())
            {
                _storeContext.BatchCreateOrUpdate(new BatchStoreChangeApiModel
                {
                    ChangeTypeId = (int)StoreChangeType.Prescription,
                    HospitalChangeGoods = goods.ToList(),
                }, model.HospitalDepartmentId, userId);
                _prescriptionRespository.UpdateStatus(id, PrescriptionStatus.Submited);
                trans.Commit();
            }
            return 0;
        }
    }
}
