using Humanity.Application;
using Humanity.Domain.Core.Models;
using Humanity.Infrastructure;
using Humanity.Infrastructure.Data;
using Humanity.WebApi.Extensions;
using Humanity.WebApi.Middlewares;
using Humanity.WebApi.StartupTasks;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var appSettings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
.Build();


builder.Services.AddDbContext<LisanssizContext>(x =>
{
    //x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    //{
    //    options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    //});


    x.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"), options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(LisanssizContext)).GetName().Name);

    });


});

builder.Services.ConfigureApplication();

builder.Services.ConfigureInfrastructure(appSettings);

builder.Services.AddHostedService<AuthenticationStartupTask>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
builder.Host.UseNLog();

var app = builder.Build();

//app.UseMiddleware<ExceptionMiddleware>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Run database migrations
await app.RunDbMigrationsAsync<LisanssizContext>();



//using (var scope = app.Services.CreateScope())
//{
//    scope.ServiceProvider.MigrateDatabase();
//}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
  );

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.UseCustomException();
app.UseAuthentication();
app.UseAuthorization();

app.Run();




