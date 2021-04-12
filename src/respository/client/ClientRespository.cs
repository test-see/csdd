﻿using domain.client.entity;
using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.client;
using irespository.client.maping.model;
using irespository.client.model;
using irespository.hospital;
using irespository.hospital.client.model;
using nouns.client.profile;
using System;
using System.Collections.Generic;
using System.Linq;

namespace respository.client
{
    public class ClientRespository : IClientRespository
    {
        private readonly DefaultDbContext _context;
        private readonly IHospitalClientRespository _hospitalClientRespository;
        public ClientRespository(DefaultDbContext context,
            IHospitalClientRespository hospitalClientRespository)
        {
            _context = context;
            _hospitalClientRespository = hospitalClientRespository;
        }
        public PagerResult<ListClientResponse> GetPagerList(PagerQuery<ListClientRequest> query)
        {
            var sql = from r in _context.Client
                      join u in _context.User on r.CreateUserId equals u.Id
                      orderby r.Id descending
                      select new ListClientResponse
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Name = r.Name,
                          CreateUserName = u.Username,
                      };
            if (!string.IsNullOrEmpty(query.Query?.Name))
            {
                sql = sql.Where(x => x.Name.Contains(query.Query.Name));
            }
            return new PagerResult<ListClientResponse>(query.Index, query.Size, sql);
        }


        public Client Update(int id, UpdateClientRequest updated, int userId)
        {
            var client = _context.Client.First(x => x.Id == id);
            client.Name = updated.Name;

            _context.Client.Update(client);
            _context.SaveChanges();

            return client;
        }

        public GetClientResponse GetIndex(int id)
        {
            var client = (from r in _context.Client
                          join u in _context.User on r.CreateUserId equals u.Id
                          where r.Id == id
                          select new GetClientResponse
                          {
                              CreateTime = r.CreateTime,
                              Id = r.Id,
                              Name = r.Name,
                              CreateUserName = u.Username,
                          }).First();

            var sql = from p in _context.Client2HospitalClient
                      join c in _context.HospitalClient on p.HospitalClientId equals c.Id
                      join h in _context.Hospital on c.HospitalId equals h.Id
                      where p.ClientId == id
                      select new ListClient2HospitalClientResponse
                      {
                          Client = new GetClientResponse
                          {
                              Id = client.Id,
                              Name = client.Name,
                          },
                          ClientMappingId = p.Id,
                          HospitalClient = new GetHospitalClientResponse
                          {
                              Id = c.Id,
                          },
                      };
            client.HospitalClients = sql.ToList();
            var clients = _hospitalClientRespository.GetValue(client.HospitalClients.Select(x => x.HospitalClient.Id).ToArray());
            foreach (var m in client.HospitalClients)
            {
                m.HospitalClient = clients.FirstOrDefault(x => x.Id == m.HospitalClient.Id);
            }

            return client;
        }

        public IList<GetClientResponse> GetValue(int[] ids)
        {
            if (ids.Length == 0) return new List<GetClientResponse>();
            var client = (from r in _context.Client
                          join u in _context.User on r.CreateUserId equals u.Id
                          where ids.Contains(r.Id)
                          select new GetClientResponse
                          {
                              Id = r.Id,
                              Name = r.Name,
                          }).ToList();

            return client;
        }


    }
}
