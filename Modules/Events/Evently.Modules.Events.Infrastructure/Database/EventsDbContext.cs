using Evently.Common.Application.Data;
using Evently.Common.Infrastructure.Outbox;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Category;
using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Infrastructure.Event;
using Evently.Modules.Events.Infrastructure.TicketTypes;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Infrastructure.Database;
public sealed class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options) , IUnitOfWork
{
    internal DbSet<Events.Domain.Event.Event> Events { get; set; }

    internal DbSet<Category> Categories { get; set; }

    internal DbSet<TicketType> TicketTypes { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());  // for implemention outBox pattern
        modelBuilder.ApplyConfiguration(new EventEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TicketTypeEntityTypeConfiguration());
    }
}

