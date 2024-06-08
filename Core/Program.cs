using Core;
using Core.Domain.Cases;
using Core.Domain.Repositories;
using Core.Domain.Services;
using Core.Infra.Repositories;
using Core.Infra.Services;
using Core.Presenters.Cases;
using Core.Presenters.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

JWTModel jwt = builder.Configuration.GetSection("JWT").Get<JWTModel>();

string connectionString = builder.Configuration.GetConnectionString("MySQL");
IDbConnection connection = new MySqlConnection(connectionString);

Random rnd = new();

builder.Services.AddSingleton<IDbConnection>(connection);
builder.Services.AddSingleton<JWTModel>(jwt);
builder.Services.AddSingleton(rnd);

// Services

builder.Services.AddTransient<IGenerateDeliveryCodeService, GenerateDeliveryCodeService>();

// Repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddTransient<IPositionDeliveryRepository, PositionDeliveryRepository>();
builder.Services.AddTransient<IAddressRepository, AddressRepository>();

// Use Cases
builder.Services.AddTransient<ISignInCase, SignInCase>();
builder.Services.AddTransient<ISignUpCase, SignUpCase>();
builder.Services.AddTransient<IGetUserInfoCase, GetUserInfoCase>();
builder.Services.AddTransient<IAddDeliveryCase, AddDeliveryCase>();
builder.Services.AddTransient<ISearchDeliveryCase, SearchDeliveryCase>();
builder.Services.AddTransient<IGetNotSavedDeliveryCase, GetNotSavedDeliveryCase>();
builder.Services.AddTransient<IGetSavedDeliveryCase, GetSavedDeliveryCase>();
builder.Services.AddTransient<IGetDetailedSavedDeliveryCase, GetDetailedSavedDelivery>();
builder.Services.AddTransient<IUpdateDeliveryStatusCase, UpdateDeliveryStatusCase>();
builder.Services.AddTransient<IGetCourierDeliveriesCase, GetCourierDeliveriesCase>();
builder.Services.AddTransient<IAddNewPositionCase, AddNewPositionCase>();
builder.Services.AddTransient<IGetDeliveryDetailsByIdCase, GetDeliveryDetailsByIdCase>();
builder.Services.AddTransient<IGetAllSavedDeliveriesCase, GetAllSavedDeliveriesCase>();
builder.Services.AddTransient<IUserSaveDeliveryCase, UserSaveDeliveryCase>();
builder.Services.AddTransient<IGetUserDeliveriesCase, GetUserDeliveriesCase>();
builder.Services.AddTransient<IDeleteFromSavedCase, DeleteFromSavedCase>();
builder.Services.AddTransient<IUpdateDeliveryCase, UpdateDeliveryCase>();
builder.Services.AddTransient<IDeleteDeliveryCase, DeleteDeliveryCase>();
builder.Services.AddTransient<IGetDeliveryByIdCase, GetDeliveryByIdCase>();
builder.Services.AddTransient<IChangePasswordCase, ChangePasswordCase>();
builder.Services.AddTransient<IResetPasswordCase, ResetPasswordCase>();

// Add services to the container.
// Isso aqui � para converter DateOnly, foi a forma que achei de lidar com o erro de serialization
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

// Adding Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
        };
    });

// Middleware de exceção
builder.Services
    .AddControllersWithViews(opt =>
    {
        opt.Filters.Add<ExceptionMiddleware>();
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Trakky API", Version = "v1" });
});

// RUN IN LOCAL IP
//builder.WebHost.UseUrls("http://*:5566");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(x => x.AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
