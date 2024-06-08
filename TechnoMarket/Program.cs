using Microsoft.EntityFrameworkCore;
using TechnoMarket.Application.IServices;
using TechnoMarket.Application.Services;
using TechnoMarket.Domain.Interfaces;
using TechnoMarket.Infrastructure;
using TechnoMarket.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//inicializacion de los servicios de los controllers

#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>(); 
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
#endregion

#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IRoleService, RoleService>();
#endregion

builder.Services.AddDbContext<ApplicationContext>(dbContextOptions => dbContextOptions.UseSqlite(
builder.Configuration["ConnectionStrings:TechnoMarketDBConnectionString"], b => b.MigrationsAssembly("TechnoMarket")));// Dependencia de Context.

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
