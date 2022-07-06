using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBBSWebApi.Bll.Interfaces;
using MyBBSWebApi.Models;
using MyBBSWebApi.Models.Models;

namespace MyBBSWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostListController : Controller
    {
        public IUsersBll UserBll { get; }
        public IPostsBll PostsBll { get; }

        public PostListController(IUsersBll userBll,IPostsBll postsBll){
            UserBll = userBll;
            PostsBll = postsBll;
        }

        [HttpGet("{token}")]
        public List<Post> GetPosts(string token){
           User user = UserBll.GetUsersByToken(token);
           //获取posts
           return PostsBll.GetAll().ToList();
        }
    }


}