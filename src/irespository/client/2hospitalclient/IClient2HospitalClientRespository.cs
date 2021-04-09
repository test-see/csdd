using foundation.config;
using foundation.ef5.poco;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;

namespace irespository.client.maping
{
    public interface IClient2HospitalClientRespository
    {
        int Delete(int id);
        Client2HospitalClient Create(Client2HospitalClientCreateApiModel created, int userId);
        PagerResult<Client2HospitalClientListApiModel> GetPagerList(PagerQuery<Client2HospitalClientListQueryModel> query);
    }
}
