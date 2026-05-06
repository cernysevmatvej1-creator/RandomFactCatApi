
using ApiDeepSeek.Aplacation.InterfaceServies;
using ApiDeepSeek.Aplacation.Servies;
using ApiDeepSeek.Doamin.Interfais;
using ApiDeepSeek.Infrastructure.Repotisory;
using GroupApi.Controllers;
using GroupApi.Data; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IFactRepotisiory, FactRepotisiory>();
builder.Services.AddScoped<IUserRepotisory, UserRepotisiory>();
builder.Services.AddScoped<IFactService, FactServicecs>();
builder.Services.AddScoped<IJWTokenServiescs, JWTokenServies>();
var jwtKey = builder.Configuration["Jwt:Key"] ?? "dGhpcyBpcyBteSBzdXBlciBzZWNyZXQga2V5IGZvciBKd3QhISEhISEhISEh";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
