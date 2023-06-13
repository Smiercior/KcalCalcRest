using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using KcalCalcRest.Data;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Mappings;
using KcalCalcRest.Models;
using KcalCalcRest.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KcalCalcRest;

public class Program {
	public static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);
		var connectionString = builder.Configuration.GetConnectionString("PostgresSQL");

		// Add services to the container.
		builder.Services.AddControllers();

		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		// Add Entity Framework
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseNpgsql(connectionString));

		// Add JWT token authentication
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options => {
				options.TokenValidationParameters = new TokenValidationParameters {
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = "issuer",
					ValidAudience = "audience",
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretKey"))
				};
			});

		// Use Identity
		builder.Services.AddIdentity<User, IdentityRole>(o => {
			o.Password.RequireDigit = true;
			o.Password.RequireLowercase = true;
			o.Password.RequireUppercase = true;
			o.Password.RequireNonAlphanumeric = true;
			o.User.RequireUniqueEmail = true;
		})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();
		
		// Add services.
		builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
		var mapperConfig = new MapperConfiguration(map => {
			map.AddProfile<UserMappingProfile>();
		});
		builder.Services.AddSingleton(mapperConfig.CreateMapper()); 
		builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();
		app.UseAuthentication();

		app.MapControllers();

		app.Run();
	}
}
