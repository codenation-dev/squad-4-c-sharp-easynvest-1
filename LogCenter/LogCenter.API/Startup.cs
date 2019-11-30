using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LogCenter.App;
using LogCenter.Domain.Entities;
using LogCenter.Infra.Database;
using LogCenter.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
using Environment = LogCenter.Domain.Entities.Environment;

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
            services.AddDbContext<DatabaseContext>(options => options.UseSqlite(@"Filename=..\LogCenter.db"));

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
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey",
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =  new SymmetricSecurityKey(Encoding.ASCII.GetBytes("_n0d0nuts4U_SECRET_")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                    options.AddPolicy(JwtBearerDefaults.AuthenticationScheme,
                        builder =>
                        {
                            builder.
                            AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).
                            RequireAuthenticatedUser().
                            Build();
                        }
                    );
                }
            );

            services.AddAuthentication();

            ConfigureDI(services);
            SeedData(services);
            
            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        private void SeedData(IServiceCollection services)
        {
            var provider =  services.BuildServiceProvider();
            using(var context = provider.GetService<DatabaseContext>())
            {
                context.Database.EnsureCreated();
                if (context.Logs.Count() > 0) {
                    // Do not run seed, there is already data in db
                    return;
                }

                context.Logs.AddRange(new Log[] { 
                    new Log {
                        Title = "Starting application...",
                        Level = Domain.Enums.LevelType.Debug,
                        Origin = "http://localhost:5001",
                        Description = "Starting application on port 5001, you are ready to rock!",
                        CreationDate = DateTime.Now,
                        Environment = Environment.Dev,
                        User = new User {
                            CreationDate = DateTime.Now,
                            Email = "test@test.com",
                            Nome = "Test User",
                            Password = "P@ssw0rd",
                            Token = Guid.NewGuid().ToString()
                        }
                    },
                    new Log {
                        Title = "Example of obsolete log",
                        Level = Domain.Enums.LevelType.Debug,
                        Origin = "http://localhost:5001",
                        Description = "User scope is invalid",
                        Environment = Environment.Homologacao,
                        CreationDate = DateTime.Now.Subtract(TimeSpan.FromDays(30)),
                        Archived = true,
                        User = new User {
                            CreationDate = DateTime.Now.Subtract(TimeSpan.FromDays(30)),
                            Email = "another@test.com",
                            Nome = "Another Test User",
                            Password = "P@ssw0rd",
                            Token = Guid.NewGuid().ToString(),
                        }
                    },
                });

                context.SaveChanges();
            } 
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

            // app.UseHttpsRedirection();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration?.GetSection("Swagger:Endpoint")?.Value,
                                  Configuration?.GetSection("Swagger:Title")?.Value + " - " + Configuration?.GetSection("Swagger:Version")?.Value);
                c.DocumentTitle = Configuration?.GetSection("Swagger:Title")?.Value;
                c.DocExpansion(DocExpansion.None);
            });
            
            app.UseMvc();
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
