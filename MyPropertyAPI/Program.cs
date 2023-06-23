
using BL.Managers.PendingProperty;
using DAL.Data.Context;
using DAL.Repos.PendingProperty;

using BL.Mangers;
using DAL.Data.Context;

using DAL.Repos.Apartment;

using DAL.Repos.Users;

using Microsoft.EntityFrameworkCore;
using BL.Mangers.Users;
using Microsoft.AspNetCore.Identity;
using DAL.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System.Security.Claims;

namespace MyPropertyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Database
            var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
            builder.Services.AddDbContext<MyProperyContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IUsersRepo,UsersRepo>();
            builder.Services.AddScoped<IUersManger,UsersManger >();
            builder.Services.AddScoped<IPendingPropertyRepo, PendingPropertyRepo>();
            builder.Services.AddScoped<IPendingPropertyManager, PendingPropertyManager>();

            builder.Services.AddScoped<IApartmentRepo, ApartmentRepo>();
            builder.Services.AddScoped<IapartmentManger, ApartmentManger>();

            //cors
            /*            builder.Services.AddCors(options => options.AddPolicy("all", p => p.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));
            */



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", cors =>
                {
                    cors.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


            //Registration  
            builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.User.RequireUniqueEmail = true;
                
            })
            .AddEntityFrameworkStores<MyProperyContext>();



            //verify Token
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Cool";
                options.DefaultChallengeScheme = "Cool";
            })
            .AddJwtBearer("Cool", options =>
            {
                string keyString = builder.Configuration.GetValue<string>("SecretKey") ?? string.Empty;
                var keyInBytes = Encoding.ASCII.GetBytes(keyString);
                var key = new SymmetricSecurityKey(keyInBytes);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            // authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy
                    .RequireClaim(ClaimTypes.Role,"User"));

                options.AddPolicy("Admin", policy => policy
                    .RequireClaim(ClaimTypes.Role, "Admin"));

                options.AddPolicy("Broker", policy => policy
                .RequireClaim(ClaimTypes.Role, "Broker"));


            });






            var app = builder.Build();

/*            app.UseCors("all");
*/
            var staticFilesPath = Path.Combine(Environment.CurrentDirectory, "Photos");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesPath),
                RequestPath = "/Photos"
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}