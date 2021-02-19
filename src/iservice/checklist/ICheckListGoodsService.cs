﻿using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.model;

namespace iservice.checklist
{
    public interface ICheckListGoodsService
    {
        PagerResult<CheckListGoodsApiModel> GetPagerList(PagerQuery<CheckListGoodsQueryModel> query);
        CheckListGoods Create(CheckListGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, CheckListGoodsUpdateApiModel updated);
    }
}