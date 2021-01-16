using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using System.Collections.Generic;

namespace iservice.client
{
    public interface IClientService
    {
        PagerResult<ClientListApiModel> GetPagerList(PagerQuery<ClientListQueryModel> query);
        Client Create(ClientCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, ClientUpdateApiModel updated, int userId);
        ClientIndexApiModel GetIndex(int id);
        IList<IdNameValueModel> GetHospitalClientList();
    }
}
