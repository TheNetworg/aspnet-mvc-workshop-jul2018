using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Todo.BL;
using Todo.DAL;
using Todo.DAL.Entities;

namespace TodoWebApi
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
            services.AddDbContext<AppDbContext>();
            services.AddScoped<ITodoService, TodoServiceEF>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://login.microsoftonline.com/common/v2.0/";
                    options.Audience = "1e2bec9a-1b43-4b5a-901a-cac3f0bb00c9";
                    options.TokenValidationParameters.ValidateIssuer = true;
                    options.TokenValidationParameters.IssuerValidator = ValidateIssuer;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Todo API",
                    Version = "v1",
                    Description = "Huge API for all todo services you can imagine.",
                });
                c.AddSecurityDefinition("OAuth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"https://login.microsoftonline.com/common/oauth2/v2.0/authorize",
                    Scopes = new Dictionary<string, string>
                    {
                        {"openid", "openid" }
                    }
                });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme() { Description = "Your Bearer token obtained from Azure AD. Do not forget the correct format: Bearer <token>", In = "header", Name = "Authorization" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "OAuth2", new string[] { } },
                    { "Bearer", new string[] { } }
                });

            });

            services.AddMvc();
        }

        private string ValidateIssuer(string issuer, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            Uri uri = new Uri(issuer);
            Uri authorityUri = new Uri("https://login.microsoftonline.com/");
            string[] parts = uri.AbsolutePath.Split('/');
            if (parts.Length >= 2)
            {
                Guid tenantId;
                if (uri.Scheme != authorityUri.Scheme || uri.Authority != authorityUri.Authority)
                {
                    throw new SecurityTokenInvalidIssuerException("Issuer has wrong authority");
                }
                if (!Guid.TryParse(parts[1], out tenantId))
                {
                    throw new SecurityTokenInvalidIssuerException("Cannot find the tenant GUID for the issuer");
                }
                if (parts.Length > 2 && parts[2] != "v2.0")
                {
                    throw new SecurityTokenInvalidIssuerException("Only accepted protocol versions are AAD v1.0 or V2.0");
                }
                return issuer;
            }
            else
            {
                throw new SecurityTokenInvalidIssuerException("Unknown issuer");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            ConfigureSwagger(app);
        }

        private void ConfigureSwagger(IApplicationBuilder app)
        {
            //Swager
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TOOO API v1");
                c.OAuthClientId("1e2bec9a-1b43-4b5a-901a-cac3f0bb00c9");
            });
        }
    }
}
