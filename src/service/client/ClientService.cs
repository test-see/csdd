﻿using domain.client;
using foundation.config;
using foundation.ef5.poco;
using irespository.client.model;
using iservice.client;

namespace service.client
{
    public class ClientService : IClientService
    {
        private readonly ClientContext _clientContext;
        public ClientService(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }
        public PagerResult<ListClientResponse> GetPagerList(PagerQuery<ListClientRequest> query)
        {
            return _clientContext.GetPagerList(query);
        }
        public Client Create(ClientCreateApiModel created, int userId)
        {
            return _clientContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _clientContext.Delete(id);
        }

        public int Update(int id, ClientUpdateApiModel updated, int userId)
        {
            return _clientContext.Update(id, updated, userId);
        }

        public ClientIndexApiModel GetIndex(int id)
        {
            return _clientContext.GetIndex(id);
        }
    }
}
