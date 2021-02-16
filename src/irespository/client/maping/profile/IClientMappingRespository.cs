using foundation.ef5.poco;
using irespository.client.maping.model;

namespace irespository.client.maping
{
    public interface IClientMappingRespository
    {
        int Delete(int id);
        ClientMapping Create(ClientMappingCreateApiModel created, int userId);
    }
}
