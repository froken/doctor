using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml;

namespace Doctor.Api.Authorization
{
    public static class AuthorizationExtensions
    {
        public static void AddAuthorizationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("authorization"));
                options.UseOpenIddict();
            });
        }

        public static void AddOpenIddictWithOptions(this IServiceCollection services)
        {
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
                        options.AcceptAnonymousClients();
                        options.DisableHttpsRequirement();
                        options.AddSigningKey(ReadRsaSecurityKey());
                        options.UseMvc();
                    });
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
