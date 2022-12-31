using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;

using MTR.API;
using MTR.Core;
using MTR.Core.Abstractions;
using MTR.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// alternative way to resolve auto mapper
//var mapperConfig = new MapperConfiguration(c => c.AddProfile<MTRProfile>());
//builder.Services.AddSingleton(mapperConfig.CreateMapper());
var path = System.IO.Directory.GetCurrentDirectory();
builder.Services.AddDbContext<MTRContext>(options => { options.UseSqlite($"Data Source={path}/mtr.db"); });
builder.Services.AddAutoMapper(typeof(MTRProfile));
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddTransient<ICardManager, CardManager>();
builder.Services.AddTransient<IRoundManager, RoundManager>();
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
