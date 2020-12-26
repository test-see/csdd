﻿using foundation.config;
using irespository.sys.model;

namespace irespository.user
{
    public interface ISysWhitePhoneRespository
    {
        bool Exists(string phone);
        PagerResult<WhitePhoneListApiModel> GetPagerList(PagerQuery<WhitePhoneListQueryModel> query);
    }
}
