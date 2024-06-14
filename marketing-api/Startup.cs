using AutoMapper;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Models;
using Service.Agent;
using Service.Candidate;
using Service.CompanyClient;
using Service.JwtAuthManager;
using Service.Master;
using Service.Upload;
using Service.User;
using Service.Utility;
using System.Text;

namespace marketing_api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("EnableCORS", builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //        .AllowAnyHeader()
            //        .AllowAnyMethod();
            //    });
            //});

            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
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
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            services.AddDbContext<MarketingToolContext>();
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddHostedService<JwtRefreshTokenCache>();
            services.AddScoped<IUserInterface, UserService>();
            services.AddScoped<IMasterInterface, MasterService>();
            services.AddScoped<ICompanyclientInterface, CompanyclientService>();
            services.AddScoped<ICandidateInterface, CandidateService>();
            services.AddScoped<IAgentInterface, AgentService>();
            services.AddScoped<IUploadInterface, UploadService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<UnitOfWork>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = "/Resources"
            });
            //app.UseCors("EnableCORS");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
