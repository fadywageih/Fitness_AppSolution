using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Progress_Tracking.Features.Achievements;
using Progress_Tracking.Features.LogWeight;
using Progress_Tracking.Features.LogWorkout;
using Progress_Tracking.Features.ViewProgress;
using Progress_Tracking.Persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProgressDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddHttpContextAccessor();

var jwtSettings = builder.Configuration.GetSection("JwtOptions");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ProgressTracking", Version = "1.0" });
    c.AddSecurityDefinition("Bearer", new() { Name = "Authorization", Type = SecuritySchemeType.Http, Scheme = "Bearer", In = ParameterLocation.Header });
    c.AddSecurityRequirement(new() { { new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } });
});

var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseAuthentication(); app.UseAuthorization();
app.MapLogWeightEndpoint();
app.MapLogWorkoutEndpoint();
app.MapViewProgressEndpoint();

app.MapViewUserProgressEndpoint();
app.MapViewStatsEndpoint();
app.MapAchievementsEndpoint();
app.Run();