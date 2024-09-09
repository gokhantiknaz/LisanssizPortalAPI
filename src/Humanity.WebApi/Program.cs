using Humanity.Application;
using Humanity.Infrastructure;
using Humanity.Infrastructure.Data;
using Humanity.WebApi.Extensions;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

var appSettings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();

builder.Services.ConfigureApplication();

builder.Services.ConfigureInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
builder.Host.UseNLog();

var app = builder.Build();


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

app.MapControllers();


app.UseAuthentication();
app.UseAuthorization();

app.Run();
