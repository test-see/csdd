using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using irespository.client.profile.model;
using System.Collections.Generic;

namespace irespository.client
{
    public interface IClientRespository
    {
        PagerResult<ListClientResponse> GetPagerList(PagerQuery<ListClientRequest> query);
        Client Create(CreateClientRequest created);
        int Delete(int id);
        Client Update(int id, UpdateClientRequest updated, int userId);
        ClientIndexApiModel GetIndex(int id);
        IList<ClientValueModel> GetValue(int[] ids);
    }
}
