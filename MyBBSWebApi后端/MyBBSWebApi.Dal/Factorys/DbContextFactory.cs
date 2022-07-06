using Microsoft.EntityFrameworkCore;
using MyBBSWebApi.Dal.Core;

namespace MyBBSWebApi.Dal.Factorys
{
    public class DbContextFactory
    {
        private static MyBBSDbContext _dbContext = null;

        private DbContextFactory()//构造函数改为private，不能再new，避免new操作后，_dbContext都为null
        {
            
        }

        public static MyBBSDbContext GetDbContext(){//关跟踪，用于查
            if(_dbContext == null){
                _dbContext = new MyBBSDbContext();
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            return _dbContext;
        }
    }
}