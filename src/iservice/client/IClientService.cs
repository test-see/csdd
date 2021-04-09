using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using System.Collections.Generic;

namespace iservice.client
{
    public interface IClientService
    {
        int Update(int id, ClientUpdateApiModel updated, int userId);
        ClientIndexApiModel GetIndex(int id);
    }
}
