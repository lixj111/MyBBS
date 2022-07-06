using MyBBSWebApi.Models;
using MyBBSWebApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Bll.Interfaces
{
    public interface IUsersBll
    {
        string AddUser(User user);
        User CheckLogin(string username, string password);
        IEnumerable<User> GetAll();
        public User GetUsersByToken(string token);
        string RemoveUser(int id);
        string UpdateUser(User user);
    }
}
