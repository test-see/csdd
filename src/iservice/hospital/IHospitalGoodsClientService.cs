﻿using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.goods.model;
using irespository.hospital.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iservice.hospital
{
    public interface IHospitalGoodsClientService
    {
        PagerResult<HospitalGoodsClientListApiModel> GetPagerList(PagerQuery<HospitalGoodsClientQueryModel> query);
        HospitalGoodsClient Create(int goodsId, int clientId, int userId);
        int Delete(int id);
    }
}
