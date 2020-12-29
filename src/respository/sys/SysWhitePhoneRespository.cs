using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user;
using System;
using System.Linq;

namespace respository.user
{
    public class SysWhitePhoneRespository : ISysWhitePhoneRespository
    {
        private readonly DefaultDbContext _context;
        public SysWhitePhoneRespository(DefaultDbContext context)
        {
            _context = context;
        }
        public bool Exists(string phone)
        {
            return _context.SysWhitePhone.Where(x => x.Phone == phone).Any();
        }

        public PagerResult<WhitePhoneListApiModel> GetPagerList(PagerQuery<WhitePhoneListQueryModel> query)
        {
            var sql = from r in _context.SysWhitePhone
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new WhitePhoneListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Phone = r.Phone,
                          CreateUserName = u.Username,
                      };
            return new PagerResult<WhitePhoneListApiModel>(query.Index, query.Size, sql);
        }

        public SysWhitePhone Create(WhitePhoneCreateApiModel created, int userId)
        {
            var phone = new SysWhitePhone { Phone = created.Phone, CreateUserId = userId, CreateTime = DateTime.UtcNow };

            _context.SysWhitePhone.Add(phone);
            _context.SaveChanges();

            return phone;
        }

        public int Delete(int id)
        {
            var phone = _context.SysWhitePhone.Find(id);
            _context.SysWhitePhone.Remove(phone);
            _context.SaveChanges();
            return id;
        }
    }
}
