using AutoMapper;
using Doctor.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Doctor.Api
{
    public class Startup
    {
        public static Action<DbContextOptionsBuilder, IConfiguration> AuthorizationDbContextBuilder = 
            (DbContextOptionsBuilder options, IConfiguration configuration) => {
                options.UseSqlite(
                    configuration.GetConnectionString("authorization"),
                    x => x.MigrationsAssembly("Doctor.Database"));
                AuthorizationDbContextOptions = options.Options;
        };

        public static DbContextOptions AuthorizationDbContextOptions;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Mapper.Initialize(cfg => { cfg.AddProfile(new DoctorMapperProfile()); });

            services.AddAutoMapper();

            services.AddMemoryCache();

            ConfigureContext(services);

            services.AddCors(o => o.AddPolicy("ClientOriginPolicy", builder =>
            {
                builder.WithOrigins("http://localhost", "http://localhost:3000")

                       .AllowAnyMethod()
                       .AllowCredentials()
                       .AllowAnyHeader();
            }));

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

            services.Configure<MvcOptions>(options => {
                options.Filters.Add(new CorsAuthorizationFilterFactory("ClientOriginPolicy"));
            });

            services.AddMvc();

#if DEBUG
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#endif
        }

        public virtual void UpdateAuthorizationContextOptions(DbContextOptionsBuilder options)
        {
            AuthorizationDbContextBuilder(options, Configuration);
          //  options.UseSqlite(Configuration.GetConnectionString("authorization"), x => x.MigrationsAssembly("Doctor.Database"));
        }

        public virtual void ConfigureContext(IServiceCollection services)
        {
            

            services.AddDbContext<AuthorizationDbContext>(options =>
                UpdateAuthorizationContextOptions(options));
            //options.UseSqlServer(Configuration.GetConnectionString("authorization"), x => x.MigrationsAssembly("Doctor.Db")));
        }

        static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirectorWithStatusCode(HttpStatusCode statusCode) => context =>
        {
            context.Response.StatusCode = (int)statusCode;
            return Task.CompletedTask;
        };

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("ClientOriginPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
