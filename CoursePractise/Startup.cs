using CoursePractise.Data;
using Microsoft.EntityFrameworkCore;

namespace CoursePractise
{
    internal class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddSession(
                Options =>
                {
                    Options.IdleTimeout = TimeSpan.FromMinutes(10);
                    Options.Cookie.HttpOnly = true;
                    Options.Cookie.IsEssential = true;
                });
               
                
            services.AddDbContext<ApplicationDbContext>(context => context.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoint => endpoint.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"));

        }
    }
}