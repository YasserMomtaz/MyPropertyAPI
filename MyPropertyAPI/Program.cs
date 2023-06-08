
using DAL.Data.Context;
using DAL.Repos.Users;
using Microsoft.EntityFrameworkCore;
using BL.Mangers.Users;
using Microsoft.AspNetCore.Identity;
using DAL.Data.Models;

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

            var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
            builder.Services.AddDbContext<MyProperyContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IUsersRepo,UsersRepo>();
            builder.Services.AddScoped<IUersManger,UsersManger >();

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.User.RequireUniqueEmail = true;
            })
    .AddEntityFrameworkStores<MyProperyContext>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}