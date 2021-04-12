using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using nouns.client.profile;
using System.Collections.Generic;

namespace irespository.client
{
    public interface IClientRespository
    {
        PagerResult<ListClientResponse> GetPagerList(PagerQuery<ListClientRequest> query);
        int Delete(int id);
        GetClientResponse GetIndex(int id);
        IList<GetClientResponse> GetValue(int[] ids);
    }
}
