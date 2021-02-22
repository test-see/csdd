using domain.hospital;
using foundation.ef5.poco;
using irespository.hospital.model;
using iservice.hospital;
using System;
using System.Collections.Generic;
using System.Text;

namespace service.hospital
{
    public class HospitalGoodsClientService: IHospitalGoodsClientService
    {
        private readonly HospitalGoodsClientContext _hospitalGoodsClientContext;
        public HospitalGoodsClientService(HospitalGoodsClientContext hospitalGoodsClientContext)
        {
            _hospitalGoodsClientContext = hospitalGoodsClientContext;
        }
        public IList<HospitalGoodsClientListApiModel> GeListByGoodsId(int goodsId)
        {
            return _hospitalGoodsClientContext.GeListByGoodsId(goodsId);
        }
        public HospitalGoodsClient Create(int goodsId, int clientId, int userId)
        {
            return _hospitalGoodsClientContext.Create(goodsId, clientId, userId);
        }
        public int Delete(int id)
        {
            return _hospitalGoodsClientContext.Delete(id);
        }
    }
}
