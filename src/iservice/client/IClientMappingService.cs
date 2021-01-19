using foundation.ef5.poco;
using irespository.client.maping.model;

namespace iservice.client
{
    public interface IClientMappingService
    {
        ClientMapping Create(ClientMappingCreateApiModel created, int userId);
        int Delete(int id);
    }
}
