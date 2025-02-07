using Evently.Api.Extensions;
using Evently.Api.MiddleWare;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Events.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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

builder.Services.AddApplication([Evently.Modules.Events.Application.AssemblyReference.Assembly]);  // add an array of assembly

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

builder.Services.AddInfrastracture(
   databaseConnectionString,
   redisConnectionString);   // for dapper connectiondb which is a cross cutting concern

builder.Configuration.AddModuleConfiguration(["events"]);

#endregion

#region Health Check Registeration

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString);

#endregion

builder.Services.AddEventsModule(builder.Configuration);

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

app.Run();

