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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PostgresSQL");

// Add services to the container.
builder.Services.AddControllers(opt => {
	var policy = new AuthorizationPolicyBuilder("Bearer").RequireAuthenticatedUser().Build();
	opt.Filters.Add(new AuthorizeFilter(policy));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo {
		Title = "KcalApp API",
		Version = "v1",
		Description = "KcalApp API Services.",
		Contact = new OpenApiContact {
			Name = "KcalTeam"
		}
	});
	c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme."
	});
            
	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
		{
			new OpenApiSecurityScheme {
				Reference = new OpenApiReference {
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseNpgsql(connectionString));

// Add JWT token authentication
var jwtConfig = builder.Configuration.GetSection("JwtConfig");
var secretKey = jwtConfig["secret"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options => {
		options.TokenValidationParameters = new TokenValidationParameters {
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = "issuer",
			ValidAudience = "audience",
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
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