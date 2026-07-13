using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WorkoutCatalog.Features.BrowseExercises;
using WorkoutCatalog.Features.BrowsePlans;
using WorkoutCatalog.Features.BrowseWorkouts;
using WorkoutCatalog.Features.GetExerciseDetail;
using WorkoutCatalog.Features.GetPlanDetail;
using WorkoutCatalog.Features.GetWorkoutDetail;
using WorkoutCatalog.Features.StartSession;
using WorkoutCatalog.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkoutCatalogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddHttpContextAccessor();

var jwtSettings = builder.Configuration.GetSection("JwtOptions");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
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
    c.SwaggerDoc("v1", new() { Title = "WorkoutCatalog", Version = "1.0" });
    c.AddSecurityDefinition("Bearer", new() { Name = "Authorization", Type = SecuritySchemeType.Http, Scheme = "Bearer", In = ParameterLocation.Header });
    c.AddSecurityRequirement(new() { { new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } });
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseAuthentication(); app.UseAuthorization();
app.MapGetWorkoutDetailEndpoint();
app.MapStartSessionEndpoint();
app.MapBrowseExercisesEndpoint();
app.MapGetExerciseDetailEndpoint();
app.MapBrowsePlansEndpoint();
app.MapGetPlanDetailEndpoint();

app.Run();