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
        Client Create(CreateClientRequest created);
        int Delete(int id);
        Client Update(int id, UpdateClientRequest updated, int userId);
        GetClientResponse GetIndex(int id);
        IList<GetClientResponse> GetValue(int[] ids);
    }
}
