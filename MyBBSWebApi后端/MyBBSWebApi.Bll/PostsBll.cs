using System.Collections.Generic;
using System.Linq;
using MyBBSWebApi.Bll.Interfaces;
using MyBBSWebApi.Dal.Core;
using MyBBSWebApi.Dal.Factorys;
using MyBBSWebApi.Models.Models;
namespace MyBBSWebApi.Bll
{

    [System.ComponentModel.DataObject]
    public partial class PostsBll : IPostsBll
    {
        MyBBSDbContext context = DbContextFactory.GetDbContext();

        /// <summary>
        /// 构造方法
        /// </summary>
        public PostsBll()
        { }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        public IEnumerable<Post> GetAll()
        {
            return context.Posts.ToList();
        }

        /// <summary>
        /// 查询所有记录并分页
        /// </summary>
        public IEnumerable<Post> GetAllOfPage(int pageIndex = 1, int pageSize = 1)//调用时若没有设置参数，则为默认值
        {
            int skipNum = (pageIndex - 1) * pageSize;
            var list = context.Posts.Skip(skipNum).Take(pageSize);
            return list.ToList();
        }
    }
}