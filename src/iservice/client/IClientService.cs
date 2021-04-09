using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using System.Collections.Generic;

namespace iservice.client
{
    public interface IClientService
    {
        ClientIndexApiModel GetIndex(int id);
    }
}
