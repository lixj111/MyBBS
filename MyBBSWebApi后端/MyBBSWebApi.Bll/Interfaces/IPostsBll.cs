using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBBSWebApi.Models;
using MyBBSWebApi.Models.Models;

namespace MyBBSWebApi.Bll.Interfaces
{
    public interface IPostsBll
    {
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllOfPage(int pageIndex = 1, int pageSize = 1);
    }
}