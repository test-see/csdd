using foundation.ef5;
using foundation.ef5.poco;
using irespository.client.maping;
using irespository.client.maping.model;
using System;

namespace respository.client
{
    public class ClientMappingRespository : IClientMappingRespository
    {
        private readonly DefaultDbContext _context;
        public ClientMappingRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public ClientMapping Create(ClientMappingCreateApiModel created, int userId)
        {
            var mapping = new ClientMapping
            {
                HospitalClientId = created.HospitalClientId,
                ClientId = created.ClientId,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
            };
            _context.ClientMapping.Add(mapping);
            _context.SaveChanges();

            return mapping;
        }

        public int Delete(int id)
        {
            var mapping = _context.ClientMapping.Find(id);
            _context.ClientMapping.Remove(mapping);
            _context.SaveChanges();
            return id;
        }

    }
}
