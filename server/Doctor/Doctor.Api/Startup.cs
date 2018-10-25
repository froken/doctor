using Doctor.Api.Authorization;
using Doctor.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddOpenIddictWithOptions(Configuration);
            services.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("authorization"));
                options.UseOpenIddict();

            });

           
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AuthorizationDbContext>()
                .AddDefaultTokenProviders();

            services.AddOpenIddict()
                    .AddCore(options =>
                    {
                        options.UseEntityFrameworkCore()
                               .UseDbContext<AuthorizationDbContext>();
                    })
                    .AddServer(options =>
                    {
                        options.EnableAuthorizationEndpoint("/connect/authorize")
                               .EnableTokenEndpoint("/connect/token")
                               .EnableLogoutEndpoint("/connect/logout");
                        options.AllowPasswordFlow();
                        options.AcceptAnonymousClients();
                        options.DisableHttpsRequirement();
                        options.UseMvc();
                    });
            services.AddMvcCore();

#if DEBUG
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            // app.AddDefaultIddictApplicationAsync().GetAwaiter().GetResult();
        }
    }
}
