using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using SK.VenueBooking.API.MiddleWare;
using SK.VenueBooking.ORM;
using SK.VenueBooking.Repository;
using SK.VenueBooking.RepositoryAbstraction;
using SK.VenueBooking.Service;

namespace SK.VenueBooking.API
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration, "AzureAd");

            services.AddSingleton<IAuthorizationHandler, CustomAuthorizeUser>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Auth",
                    policy => policy.Requirements.Add(new CustomAuthorizeUser()));
            });

            services.AddControllers();
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Venue Booking", Version = "v1" });
            });
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IDatabaseWrapper, CustomDapper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<IVenueService, VenueService>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepostitory, BookRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ITenantService tenantService)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SK.VenueBooking.API v1"));
               
            }
           
            app.UseAuthentication();
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthorization();

            tenantService.LoadTenantUserCache();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
