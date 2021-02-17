﻿using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using irespository.client.profile.model;

namespace irespository.client
{
    public interface IClientRespository
    {
        PagerResult<ClientListApiModel> GetPagerList(PagerQuery<ClientListQueryModel> query);
        Client Create(ClientCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, ClientUpdateApiModel updated, int userId);
        ClientIndexApiModel GetIndex(int id);
        ClientValueModel GetValue(int id);
    }
}
