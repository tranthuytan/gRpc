using BAL.Services.Implements;
using BAL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;
using gRPCService_Assignment2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<BookStoreLab2Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookStore")));
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IPressRepository, PressRepository>();
builder.Services.AddScoped<IPressService, PressService>();

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


//authen & author
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<BookStoreCRUDService>();
app.MapGrpcService<AuthService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseAuthentication();
app.UseAuthorization();
app.Run();
