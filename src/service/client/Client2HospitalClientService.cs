using domain.client;
using foundation.config;
using foundation.ef5.poco;
using irespository.client.maping.model;
using irespository.client.maping.profile.model;
using iservice.client;

namespace service.client
{
    public class Client2HospitalClientService: IClient2HospitalClientService
    {
        private readonly Client2HospitalClientContext _clientMappingContext;
        public Client2HospitalClientService(Client2HospitalClientContext clientMappingContext)
        {
            _clientMappingContext = clientMappingContext;
        }
        public PagerResult<ListClient2HospitalClientResponse> GetPagerList(PagerQuery<Client2HospitalClientListQueryModel> query)
        {
            return _clientMappingContext.GetPagerList(query);
        }
        public Client2HospitalClient Create(Client2HospitalClientCreateApiModel created, int userId)
        {
            return _clientMappingContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _clientMappingContext.Delete(id);
        }
    }
}
