using Core;
using Core.Domain.Repositories;
using Core.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

JWTModel jwt = builder.Configuration.GetSection("JWT").Get<JWTModel>();

string connectionString = builder.Configuration.GetConnectionString("MySQL");
IDbConnection connection = new MySqlConnection(connectionString);

builder.Services.AddSingleton<IDbConnection>(connection);
builder.Services.AddSingleton<JWTModel>(jwt);

// Repositories
builder.Services.AddSingleton<IUserRepository, UserRepository>();

// Add services to the container.

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
             ClockSkew = TimeSpan.Zero
         }
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
