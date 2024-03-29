﻿using domain.store;
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
using System.Threading.Tasks;

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

        public async Task<PagerResult<PrescriptionListApiModel>> GetPagerListAsync(PagerQuery<PrescriptionListQueryModel> query, int hospitalId)
        {
            return await _prescriptionRespository.GetPagerListAsync(query, hospitalId);
        }

        public Prescription Create(PrescriptionCreateApiModel created, int departmentId, int userId)
        {
            return _prescriptionRespository.Create(created, departmentId, userId);
        }

        public async Task<int> SubmitAsync(int id, int userId)
        {
            var model = await _prescriptionRespository.GetIndexAsync(id);
            var list = await _prescriptionGoodsRespository.GetListAsync(id);
            var goods = list.Select(x => new StoreChangeGoodsValueModel
            {
                HospitalGoodId = x.HospitalGoods.Id,
                ChangeQty = x.Qty,
                Recrdno = RecordNumber.Next((int)StoreChangeType.Prescription, x.Id),
            });
            using (var trans = _defaultDbTransaction.Begin())
            {
                _storeContext.BatchCreateOrUpdate(new BatchStoreChangeApiModel
                {
                    ChangeTypeId = (int)StoreChangeType.Prescription,
                    HospitalChangeGoods = goods.ToList(),
                }, model.HospitalDepartment.Id, userId);
                _prescriptionRespository.UpdateStatus(id, PrescriptionStatus.Submited);
                trans.Commit();
            }
            return 0;
        }
        public async Task<PrescriptionIndexApiModel> GetIndexAsync(int id)
        {
            return await _prescriptionRespository.GetIndexAsync(id);
        }
    }
}
