using AutoMapper;
using Doctor.Database;
using Doctor.Server.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Doctor.Server
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
            Mapper.Initialize(cfg => { cfg.AddProfile(new DoctorMapperProfile()); });

            services.AddAutoMapper();

            services.AddMemoryCache();
            ConfigureDbContext(services);
			
			services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AuthorizationDbContext>();
			
			services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });
			
			services.ConfigureApplicationCookie(options => {
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToAccessDenied = ReplaceRedirectorWithStatusCode(HttpStatusCode.Forbidden),
                    OnRedirectToLogin = ReplaceRedirectorWithStatusCode(HttpStatusCode.Unauthorized)
                };

                options.Cookie.Name = ".doctor";
                options.Cookie.HttpOnly = true; 
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 

                options.SlidingExpiration = true;
            });
			
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc()
                .AddRazorPagesOptions(options =>
                    {
                        options.Conventions.AddPageRoute("/Index", "{*url:regex(^(?!api).*$)}");
                    });
        }
		
		public virtual void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<AuthorizationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("authorization"),
                    x => x.MigrationsAssembly("Doctor.Database")));
        }

        static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirectorWithStatusCode(HttpStatusCode statusCode) => context =>
        {
            context.Response.StatusCode = (int)statusCode;
            return Task.CompletedTask;
        };


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHotModuleReplacement();
				app.UseStaticFiles();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
			app.UseHttpsRedirection();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
            {
                builder.UseMvc(routes =>
                {
                    routes.MapSpaFallbackRoute(
                        name: "spa-fallback",
                        defaults: new { controller = "Home", action = "Index" });
                });
            });
        }
    }
}
