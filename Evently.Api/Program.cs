using Evently.Api.Extensions;
using Evently.Api.MiddleWare;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Events.Infrastructure;
using Evently.Modules.Ticketing.Infrastracture;
using Evently.Modules.Users.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region serialog config

builder.Host.UseSerilog((context , logConfig) => logConfig.ReadFrom.Configuration(context.Configuration));

#endregion

#region GlobaExceptionHandling

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region common Cross Cutting Concerns

builder.Services.AddApplication([
    Evently.Modules.Events.Application.AssemblyReference.Assembly,
    Evently.Modules.Users.Application.AssemblyReference.Assembly,
    Evently.Modules.Ticketing.Application.AssemblyReference.Assembly]);  // add an array of assembly

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;
string mongoConnectionString = builder.Configuration.GetConnectionString("Mongo")!;

builder.Services.AddInfrastracture(
    [TicketingModule.ConfigureConsumers],
   databaseConnectionString,
   redisConnectionString,
   mongoConnectionString);   // for dapper connectiondb which is a cross cutting concern

builder.Configuration.AddModuleConfiguration(["events" , "users" , "ticketing"]);  // register modules json files automatically

#endregion

#region Health Check Registeration

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString)
    .AddMongoDb(_ => new MongoDB.Driver.MongoClient(mongoConnectionString));

#endregion

// adding modules here 
builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);




WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}


app.MapEndPoints();   // configure all minimal api endpoints

app.MapHealthChecks("health" , new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.UseSerilogRequestLogging();    // add logging middleware

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.Run();

