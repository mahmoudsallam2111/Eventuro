using Evently.Api.Extensions;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Modules.Events.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region serialog config

builder.Host.UseSerilog((context , logConfig) => logConfig.ReadFrom.Configuration(context.Configuration));   

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region common Cross Cutting Concerns

builder.Services.AddApplication([Evently.Modules.Events.Application.AssemblyReference.Assembly]);  // add an array of assembly
builder.Services.AddInfrastracture(builder.Configuration.GetConnectionString("Database")!);   // for dapper connectiondb which is a cross cutting concern

builder.Configuration.AddModuleConfiguration(["events"]);

#endregion

builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}


EventsModule.MapEndpoints(app);

app.UseSerilogRequestLogging();    // add logging middleware

app.Run();

