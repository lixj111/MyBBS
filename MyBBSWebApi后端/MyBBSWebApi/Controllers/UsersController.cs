using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBBSWebApi.Bll.Interfaces;
using MyBBSWebApi.Models.Models;
using MyBBSWebApi.ViewModels;

namespace MyBBSWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersBll _usersBll;

        public UsersController(IUsersBll UsersBll){
            _usersBll = UsersBll;
        }

        [HttpPost]
        public bool UserEdit(UserEditViewModel edituser){//在用户视图下可修改的信息
            try{
                User user = _usersBll.GetAll().FirstOrDefault(m=>m.Id==edituser.Id);//firstordefault()返回找到的第一个或不包含元素的默认的序列
                user.UserName=edituser.UserName;
                if(edituser.Password!=null && edituser.Password.Trim()!="")//trim()去除空格
                {
                    user.Password=edituser.Password;
                }
                _usersBll.UpdateUser(user);
                return true;
            }
            catch{
                return false;
            }
        }




    }
}