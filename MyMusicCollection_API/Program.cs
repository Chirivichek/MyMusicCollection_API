using System.Reflection;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
var builder = WebApplication.CreateBuilder(args);

string connSrt = builder.Configuration.GetConnectionString("MyMusicCollection");
// Add services to the container.  

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Fix: Use builder.Services to call AddDbContext  
builder.Services.AddDbContext<MusicCollectionBDcontext>(cfg => cfg.UseSqlServer(connSrt));

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
