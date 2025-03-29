using System.Data.Common;
using Evently.Common.Infrastructure.Outbox;
using Evently.Modules.Ticketing.Application.Abstractions.Data;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Domain.Payments;
using Evently.Modules.Ticketing.Domain.Ticketing;
using Evently.Modules.Ticketing.Infrastracture.Customers;
using Evently.Modules.Ticketing.Infrastracture.Events;
using Evently.Modules.Ticketing.Infrastracture.Orders;
using Evently.Modules.Ticketing.Infrastracture.Payments;
using Evently.Modules.Ticketing.Infrastracture.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Evently.Modules.Ticketing.Infrastracture.Database;
public sealed class TicketingDbContext(DbContextOptions<TicketingDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Customer> Customers { get; set; }
    internal DbSet<Event> Events { get; set; }
    internal DbSet<TicketType> TicketTypes { get; set; }
    internal DbSet<Order> Orders { get; set; }

    internal DbSet<OrderItem> OrderItems { get; set; }

    internal DbSet<Ticket> Tickets { get; set; }

    internal DbSet<Payment> Payments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Ticketing);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());  // for implemention outBox pattern
        modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
    }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}
