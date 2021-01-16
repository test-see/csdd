using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user;
using System;
using System.Linq;

namespace respository.user
{
    public class SysConfigRespository : ISysConfigRespository
    {
        private readonly DefaultDbContext _context;
        public SysConfigRespository(DefaultDbContext context)
        {
            _context = context;
        }

        public PagerResult<ConfigListApiModel> GetPagerList(PagerQuery<ConfigListQueryModel> query)
        {
            var sql = from r in _context.SysConfig
                      join u in _context.User on r.CreateUserId equals u.Id
                      select new ConfigListApiModel
                      {
                          CreateTime = r.CreateTime,
                          Id = r.Id,
                          Value = r.Value,
                          Key = r.Key,
                          Remark = r.Remark,
                          CreateUserName = u.Username,
                      };
            return new PagerResult<ConfigListApiModel>(query.Index, query.Size, sql);
        }

        public SysConfig Create(ConfigCreateApiModel created, int userId)
        {
            var config = new SysConfig
            {
                Value = created.Value,
                Key = created.Key,
                Remark = created.Remark,
                CreateUserId = userId,
            };

            _context.SysConfig.Add(config);
            _context.SaveChanges();

            return config;
        }

        public int Delete(int id)
        {
            var config = _context.SysConfig.Find(id);
            _context.SysConfig.Remove(config);
            _context.SaveChanges();
            return id;
        }
    }
}
