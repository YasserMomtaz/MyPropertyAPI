
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

            //cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", cors =>
                {
                    cors.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


            //Registration  (msht8ltsh 8er lma 5letha user)
            builder.Services.AddIdentity<User,IdentityRole>(options =>
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




            builder.Services.AddScoped<IApartmentRepo, ApartmentRepo>();
            builder.Services.AddScoped<IapartmentManger, ApartmentManger>();
            
            var app = builder.Build();

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