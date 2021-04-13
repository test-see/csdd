using foundation.ef5;
using foundation.ef5.poco;
using irespository.hospital;
using irespository.hospital.client.model;
using System;
using System.Linq;

namespace respository.hospital
{
    public class HospitalClientRespository : IHospitalClientRespository
    {
        private readonly DefaultDbContext _context;
        public HospitalClientRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public HospitalClient Create(HospitalClientCreateApiModel created, int userId)
        {
            var goods = new HospitalClient
            {
                Name = created.Name,
                HospitalId = created.HospitalId,
                CreateUserId = userId,
                CreateTime = DateTime.Now,
            };

            _context.HospitalClient.Add(goods);
            _context.SaveChanges();

            return goods;
        }



        public int Delete(int id)
        {
            var goods = _context.HospitalClient.Find(id);
            _context.HospitalClient.Remove(goods);
            _context.SaveChanges();
            return id;
        }

        public int Update(int id, HospitalClientUpdateApiModel updated)
        {
            var goods = _context.HospitalClient.First(x => x.Id == id);

            goods.Name = updated.Name;

            _context.HospitalClient.Update(goods);
            _context.SaveChanges();
            return goods.Id;
        }
    }
}
