using API.Data;
using API.DbInistializer;
using API.Interfaces;
using API.models;
using API.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
           var connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' noot found");

            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(connectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();//bringin the application user and identity role from the DataContext
            services.AddScoped<IDbInitializer,API.DbInistializer.DbInitializer>(); //register the database initializer for dependency injection
            services.AddCors();// allow cross-origin requests react pour le front-end 
            services.AddControllers();
            //configuer les sessionq options
            services.AddDistributedMemoryCache(); // in-memory cache for session state
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); 
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
            });
            services.AddScoped<ItokenService, TokenService>(); //register the token service for dependency injection
            var secretKey = config["AppSettings:TokenKey"]
                ?? throw new ArgumentNullException("Token key is not configured in AppSettings.");
            
                
                
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });





            return services;
        }
    }
}
