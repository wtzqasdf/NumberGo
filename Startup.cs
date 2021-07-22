using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using NumberGo.Models.Contexts;
using NumberGo.Models.Repositories;
using NumberGo.Utils;

namespace NumberGo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAntiforgery(a =>
            {
                a.HeaderName = "c_token";
                a.FormFieldName = "c_token";
                a.Cookie.Name = "c_token";
            });
            services.AddSession(s =>
            {
                s.Cookie.Name = "ses";
                s.Cookie.HttpOnly = true;
                s.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
                s.IdleTimeout = new TimeSpan(1, 0, 0);
            });
            services.AddScoped<MailSender>(new Func<IServiceProvider, MailSender>((ms) =>
            {
                return new MailSender(Configuration["MailHost"], int.Parse(Configuration["MailPort"]), Configuration["MailSender"], Configuration["MailSenderName"]);
            }));
            //sql setting
            string conn = Configuration.GetConnectionString("mysql");
            services.AddDbContext<UserContext>(act =>
            {
                act.UseMySql(conn, ServerVersion.AutoDetect(conn));
            });
            services.AddDbContext<OrderContext>(act => 
            {
                act.UseMySql(conn, ServerVersion.AutoDetect(conn));
            });
            services.AddDbContext<ScoreContext>(act =>
            {
                act.UseMySql(conn, ServerVersion.AutoDetect(conn));
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error500");

                app.UseHsts();
            }
            app.UseStatusCodePagesWithRedirects("~/Error{0}");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseSession();

            app.UseEndpoints(SetEndPoints);
        }

        private void SetEndPoints(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder builder)
        {
            builder.MapControllerRoute(
                name: "Home",
                pattern: "{action}/{id}",
                defaults: new { controller = "Home", action = "action", id = "id" });

            builder.MapControllerRoute(
                name: "Error",
                pattern: "{action}",
                defaults: new { controller = "Error", action = "action" });

            builder.MapControllerRoute(
                name: "User",
                pattern: "user/{action}",
                defaults: new { controller = "User", action = "action" });

            builder.MapControllerRoute(
                name: "Sound",
                pattern: "sound/{action}",
                defaults: new { controller = "Sound", action = "action" });

            builder.MapControllerRoute(
                name: "Score",
                pattern: "score/{action}",
                defaults: new { controller = "Score", action = "action" });

            builder.MapControllerRoute(
                name: "Trade",
                pattern: "trade/{action}",
                defaults: new { controller = "Trade", action = "action" });

            builder.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
        }
    }
}
