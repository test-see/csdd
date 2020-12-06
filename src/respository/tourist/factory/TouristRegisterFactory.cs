using foundation.ef5;
using foundation.ef5.poco;
using irespository.tourist.factory;
using irespository.tourist.model;
using System;
using System.Threading.Tasks;

namespace respository.tourist
{
    public class TouristRegisterFactory : ITouristRegisterFactory
    {
        private readonly DefaultDbContext _context;
        public TouristRegisterFactory(DefaultDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateTouristAsync(TouristRegisterApiModel tourist)
        {
            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                var profile = await _context.Tourist.AddAsync(new Tourist
                {
                    IdentityCategoryId = tourist.Profile.IdentityCategoryId,
                    Phone = tourist.Profile.Phone,
                    ProvinceId = tourist.Profile.ProvinceId,
                    Status = 0,
                    Username = tourist.Profile.Username,
                    CreateTime = DateTime.UtcNow,
                });
                _context.SaveChanges();
                foreach (var client in tourist.ClientPreferences)
                {
                    await _context.TouristClientPreference.AddAsync(new TouristClientPreference
                    {
                        ClientId = client.ClientId,
                        HospitalClientId = client.HospitalClientId,
                        TouristId = profile.Entity.Id,
                    });
                }
                foreach (var hospital in tourist.HospitalPreferences)
                {
                    await _context.TouristHospitalPreference.AddAsync(new TouristDepartmentPreference
                    {
                        DepartmentId = hospital.DepartmentId,
                        TouristId = profile.Entity.Id,
                    });
                }
                foreach (var sales in tourist.SalesPreferences)
                {
                    await _context.TouristSalesPreference.AddAsync(new TouristSalesPreference
                    {
                        HospitalGoodsId = sales.HospitalGoodsId,
                        TouristId = profile.Entity.Id,
                    });
                }
                _context.SaveChanges();
                await trans.CommitAsync();
                return profile.Entity.Id;
            }
        }
    }
}
