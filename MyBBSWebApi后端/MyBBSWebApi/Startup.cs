using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyBBSWebApi.Bll;
using MyBBSWebApi.Bll.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //后端解决跨域
            services.AddCors(c=>c.AddPolicy("any",p=>p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            //依赖注入、三种：
            services.AddSingleton<IUsersBll,UsersBll>();//UserBll注入IUserBll
            services.AddSingleton<IPostsBll,PostsBll>();
            //services.AddSingleton();//单例，实例对象创建后一直在内存保留，不再注销
            //services.AddScoped();//单例，线程一次请求中，实例对象创建后不再注销（线程结束后应该注销？？？）
            //services.AddTransient();//使用后立即注销

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBBSWebapi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBBSWebapi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            //后端解决跨域
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
