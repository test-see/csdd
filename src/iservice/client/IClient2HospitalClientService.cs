using foundation.config;
using foundation.ef5.poco;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;

namespace iservice.client
{
    public interface IClient2HospitalClientService
    {
        Client2HospitalClient Create(Client2HospitalClientCreateApiModel created, int userId);
        int Delete(int id);
        PagerResult<Client2HospitalClientListApiModel> GetPagerList(PagerQuery<Client2HospitalClientListQueryModel> query);
    }
}
