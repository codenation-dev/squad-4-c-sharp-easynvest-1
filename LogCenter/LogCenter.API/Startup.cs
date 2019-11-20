using System.IO;
using System.Text;
using LogCenter.App;
using LogCenter.Infra.Database;
using LogCenter.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace LogCenter.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region JWT
            //TODO configurar o JWT
            //pega as configurações do appsettings.json
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret); //cria uma chave com o secret

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true, //valida o emissor 
                    ValidateAudience = true, //valida a url
                    ValidAudience = appSettings.ValidoEm, //informo qual é a url
                    ValidIssuer = appSettings.Emissor //informo qual é o emissor do token
                };
            });
            #endregion
        

        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info
                {
                    Title = Configuration?.GetSection("Swagger:Title")?.Value,
                    Version = Configuration?.GetSection("Swagger:Version")?.Value
                });

                c.SchemaRegistryOptions.CustomTypeMappings.Add(typeof(IFormFile), () => new Schema() { Type = "file", Format = "binary" });

                string xmlPath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(".dll", ".xml");
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);

                c.DescribeAllEnumsAsStrings();
            });

            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            ConfigureDI(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); // autenticação
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration?.GetSection("Swagger:Endpoint")?.Value,
                                  Configuration?.GetSection("Swagger:Title")?.Value + " - " + Configuration?.GetSection("Swagger:Version")?.Value);
                c.DocumentTitle = Configuration?.GetSection("Swagger:Title")?.Value;
                c.DocExpansion(DocExpansion.None);
            });
        }

        public void ConfigureDI(IServiceCollection services)
        {
            //Apps
            services.AddTransient<LogApp, LogApp>();
            
            //Repositories
            services.AddTransient<LogRepository, LogRepository>();

            //Services
        }
    }
}
