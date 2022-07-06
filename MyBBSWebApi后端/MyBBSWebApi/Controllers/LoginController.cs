using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyBBSWebApi.Bll.Interfaces;
using MyBBSWebApi.Common;
using MyBBSWebApi.Dal;
using MyBBSWebApi.Models;
using MyBBSWebApi.Models.Models;
using MyBBSWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class LoginController : ControllerBase
    {
        private readonly IUsersBll _usersBll;

        //构造函数，依赖UserBll注入
        public LoginController(IUsersBll usersBll)//错误CS0051 可访问性不一致: 参数类型“IUsersBll”的可访问性低于方法“LoginController.LoginController(IUsersBll)”
                                                  //解决：IUserBll的声明处加public
        {
            _usersBll = usersBll;
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            //去除new对象过程，解耦合，使程序低耦合
            IEnumerable<User> usersList = _usersBll.GetAll();
            return usersList;
        }

        [HttpGet("{userName}/{password}")]
        public User Get(string userName, string password)
        {
            User users = _usersBll.CheckLogin(userName, password);
            return users;
        }

        [HttpPost]
        public string Insert(User user)
        {
            return _usersBll.AddUser(user);
        }

        [HttpPut]
        public string Update(User user)
        {
            return _usersBll.UpdateUser(user);
        }

        [HttpDelete]
        public string Remove(int id)
        {
            return _usersBll.RemoveUser(id);
        }
    }
}
