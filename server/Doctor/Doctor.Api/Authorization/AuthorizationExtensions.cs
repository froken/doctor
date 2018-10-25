using Doctor.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Doctor.Api.Authorization
{
    public static class AuthorizationExtensions
    {
        public static void AddOpenIddictWithOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });

            services.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("authorization"));
                options.UseOpenIddict();

            });

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
                        options.AllowImplicitFlow();
                        options.AllowPasswordFlow();
                        options.AcceptAnonymousClients();
                        options.DisableHttpsRequirement();
                        options.AddSigningKey(ReadRsaSecurityKey());
                        options.UseMvc();
                    });
        }

        public static async Task AddDefaultIddictApplicationAsync(this IApplicationBuilder app)
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                if (await manager.FindByClientIdAsync("doctor-app", CancellationToken.None) == null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "doctor-app",
                        DisplayName = "Doctor Application",
                        PostLogoutRedirectUris = { new Uri("https://oidcdebugger.com/debug") },
                        RedirectUris = { new Uri("https://oidcdebugger.com/debug") },
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Authorization,
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.Endpoints.Logout,
                            OpenIddictConstants.Permissions.GrantTypes.Implicit,
                            OpenIddictConstants.Permissions.GrantTypes.Password,
                            OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode
                        }
                    };

                    await manager.CreateAsync(descriptor, CancellationToken.None);
                }

                if (await manager.FindByClientIdAsync("doctor-app-users", CancellationToken.None) == null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "doctor-app-users",
                        DisplayName = "Doctor Application Users",
                        Type = "confidential",
                        PostLogoutRedirectUris = { new Uri("https://oidcdebugger.com/debug") },
                        RedirectUris = { new Uri("https://oidcdebugger.com/debug") },
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Authorization,
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.Endpoints.Logout,
                            OpenIddictConstants.Permissions.GrantTypes.Implicit,
                            OpenIddictConstants.Permissions.GrantTypes.Password,
                            OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode
                        }
                    };

                    await manager.CreateAsync(descriptor, CancellationToken.None);
                }
            }
        }

        public static RsaSecurityKey ReadRsaSecurityKey()
        {
            var xmlString = "";

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Doctor.Api.Authorization.privatekey.xml"))
            {
                using (var sw = new StreamReader(stream))
                {
                    xmlString = sw.ReadToEnd();
                }
            }

            RSA rsa = RSA.Create();
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "Exponent": parameters.Exponent = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "P": parameters.P = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "Q": parameters.Q = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "DP": parameters.DP = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "DQ": parameters.DQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "InverseQ": parameters.InverseQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        case "D": parameters.D = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
            return new RsaSecurityKey(rsa);
        }

    }
}
