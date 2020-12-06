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
                var profile = new Tourist
                {
                    IdentityCategoryId = tourist.Profile.IdentityCategoryId,
                    Phone = tourist.Profile.Phone,
                    ProvinceId = tourist.Profile.ProvinceId,
                    Status = 0,
                    Username = tourist.Profile.Username,
                    CreateTime = DateTime.UtcNow,
                };
                await _context.Tourist.AddAsync(profile);
                await _context.SaveChangesAsync();
                foreach (var client in tourist.ClientPreferences)
                {
                    await _context.TouristClientPreference.AddAsync(new TouristClientPreference
                    {
                        ClientId = client.ClientId,
                        HospitalClientId = client.HospitalClientId,
                        TouristId = profile.Id,
                    });
                }
                foreach (var hospital in tourist.HospitalPreferences)
                {
                    await _context.TouristHospitalPreference.AddAsync(new TouristDepartmentPreference
                    {
                        DepartmentId = hospital.DepartmentId,
                        TouristId = profile.Id,
                    });
                }
                foreach (var sales in tourist.SalesPreferences)
                {
                    await _context.TouristSalesPreference.AddAsync(new TouristSalesPreference
                    {
                        HospitalGoodsId = sales.HospitalGoodsId,
                        TouristId = profile.Id,
                    });
                }
                await _context.SaveChangesAsync();
                await trans.CommitAsync();
                return profile.Id;
            }
        }
    }
}
