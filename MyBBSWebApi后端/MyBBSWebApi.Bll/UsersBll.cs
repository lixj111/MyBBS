using MyBBSWebApi.Bll.Interfaces;
using MyBBSWebApi.Common;
using MyBBSWebApi.Dal.Core;
using MyBBSWebApi.Dal.Factorys;
using MyBBSWebApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBBSWebApi.Bll
{
    public class UsersBll : IUsersBll
    {
        MyBBSDbContext context = DbContextFactory.GetDbContext();

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList().FindAll(m => !m.IsDelete);
        }

        public User CheckLogin(string username, string password)
        {
            User user = context.Users.FirstOrDefault(m => m.UserName == username && m.Password == password);//第一种用加密前密码，数据库中密码未加密
            //first如没有取到，则抛出异常；firstordefault如果没有找到，集合元素是引用类型，返回null，是值类型，返回默认值
            if (user == null)
            {
                user = context.Users.FirstOrDefault(m => m.UserName == username && m.Password == password.ToMd5());//第二种数据库密码改为加密后的
                if (user == null)
                {
                    Guid? atl = (Guid?)SqlHelper.FromDbValue(password);
                    user = context.Users.FirstOrDefault(m => m.UserName == username && m.AutoLoginTag == atl);//第三种用ALT自动登录时
                    if (user == null || atl == null)//避免数据库alt为空时，输入空字符串时登陆成功
                    {
                        return default;//密码错误，登陆失败
                    }
                    if (user.AutoLoginLimitTime < DateTime.Now)
                    {
                        return default;//自动登录过期
                    }
                    else
                    {
                        return GetLoginResult(user, false, 3);
                    }
                }
                else
                {
                    return GetLoginResult(user, true, 2);
                }
            }
            else
            {
                return GetLoginResult(user, true, 1);
            }
        }

        private User GetLoginResult(User user, bool isPasswordLogin, int passwordCategory)
        {
            if (user.IsDelete)//筛选：用户列表中IsDelete为False的用户
            {
                return default;
            }
            user.Token = Guid.NewGuid();
            if (isPasswordLogin)
            {//密码登录
                if (passwordCategory == 1)
                {
                    user.Password = user.Password.ToMd5();//数据库密码未加密前
                }
                user.AutoLoginTag = Guid.NewGuid(); //如果是通过密码登录，生成新的ALT值；反之不改变
                user.AutoLoginLimitTime = DateTime.Now.AddDays(7);//7天内免密登录
            }
            context.Users.Update(user);
            context.SaveChanges();
            return user;
        }

        public User GetUsersByToken(string token)
        {
            Guid? token1 = (Guid?)SqlHelper.FromDbValue(token);
            User user = context.Users.FirstOrDefault(m => m.Token == token1);
            if (user == null)
            {
                throw new Exception("token错误");
            }
            return user;
        }

        //不关跟踪，用于增删改
        public string AddUser(User user)
        {
            using MyBBSDbContext myBBSDbContext = new();
            user.IsDelete = false;
            myBBSDbContext.Users.Add(user);
            myBBSDbContext.SaveChanges();
            return "数据插入成功!";//一定插入成功吗？只要通过前端限制，使输入的数据一定合法即可
        }

        public string UpdateUser(User user)
        {
            using MyBBSDbContext myBBSDbContext = new();
            /*
            如果修改固定项，name+password
            User user1 = myBBSDbContext.Users.ToList().Find(m=>m.Id==user.Id);
            user1.UserName = user.UserName;
            user1.Password = user.Password;
            */
            myBBSDbContext.Users.Update(user);
            myBBSDbContext.SaveChanges();
            return "修改成功!";
        }

        public string RemoveUser(int id)
        {
            using MyBBSDbContext myBBSDbContext = new();
            User user = myBBSDbContext.Users.First(m => m.Id == id);
            myBBSDbContext.Users.Remove(user);
            myBBSDbContext.SaveChanges();
            return "删除成功!";
        }
    }
}
