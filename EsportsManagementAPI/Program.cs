/*
 * Name: Yuhao Peng
 * Date: 2023-04-05
 * */

using EsportsManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddJsonOptions(o =>
	{
		o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	});

builder.Services.AddDbContext<EsportsManagementContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("EsportsManagementContext")));

//To give access to IHttpContextAccessor for Audit Data with IAuditable
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//seed data
//comment out when publish to Azure
EsportsManagementInitializer.Seed(app);

app.Run();
