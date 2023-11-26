using Core;
using Core.Domain.Cases;
using Core.Domain.Repositories;
using Core.Infra.Repositories;
using Core.Presenters.Cases;
using Core.Presenters.Middlewares;
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

// Use Cases
builder.Services.AddSingleton<ISignInCase, SignInCase>();
builder.Services.AddSingleton<ISignUpCase, SignUpCase>();

// Add services to the container.
// Isso aqui é para converter DateOnly, foi a forma que achei de lidar com o erro de serialization
builder.Services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
               });

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

builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add<ExceptionMiddleware>();
});

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

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
