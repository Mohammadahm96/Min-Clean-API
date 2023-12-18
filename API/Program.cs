using Application;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Application.Commands.Users.Register;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Min ConnectionString
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("Server=localhost;Port=3306;Database=Cleandb;User=root;Password=12345;");


builder.Services.AddApplication().AddInfrastructure();

// Register CleanApiMainContext
builder.Services.AddDbContext<CleanApiMainContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
});

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
