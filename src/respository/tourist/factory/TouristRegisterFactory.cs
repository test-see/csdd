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
               var data = await _context.Tourist.AddAsync(new Tourist
                {
                    IdentityCategoryId = tourist.Profile.IdentityCategoryId,
                    Phone = tourist.Profile.Phone,
                    ProvinceId = tourist.Profile.ProvinceId,
                    Status = 0,
                    Username = tourist.Profile.Username,
                    CreateTime = DateTime.UtcNow,
                });
                foreach (var client in tourist.TouristClientPreferences)
                {
                    client.TouristId = data.Entity.Id;
                    await _context.TouristClientPreference.AddAsync(client);
                }
                foreach (var hospital in tourist.TouristHospitalPreferences)
                {
                    hospital.TouristId = data.Entity.Id;
                    await _context.TouristHospitalPreference.AddAsync(hospital);
                }
                foreach (var sales in tourist.TouristSalesPreferences)
                {
                    sales.TouristId = data.Entity.Id;
                    await _context.TouristSalesPreference.AddAsync(sales);
                }
                await trans.CommitAsync();
                return data.Entity.Id;
            }
        }
    }
}
