using Evently.Api.Extensions;
using Evently.Common.Application;
using Evently.Modules.Events.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplication([Evently.Modules.Events.Application.AssemblyReference.Assembly]);  // add an array of assembly

builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}


EventsModule.MapEndpoints(app);

app.Run();

